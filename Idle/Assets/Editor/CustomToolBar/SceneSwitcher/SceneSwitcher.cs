using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UnityToolbarExtender.Examples
{
	static class ToolbarStyles
	{
		public static readonly GUIStyle commandButtonStyle;

		static ToolbarStyles()
		{
			commandButtonStyle = new GUIStyle("Command")
			{
				fontSize = 16,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageAbove,
				fontStyle = FontStyle.Bold
			};
		}
	}

	[InitializeOnLoad]
	public class SceneSwitchLeftButton
	{
		class SceneData
		{
			public string     path;
			public GUIContent popupDisplay;
		}

		public string NameInList => "[Dropdown] Scene selection";

		static bool showSceneFolder = true;

		static SceneData[] scenesPopupDisplay;
		static string[]    scenesPath;
		static string[]    scenesBuildPath;
		static int                selectedSceneIndex;

		static SceneSwitchLeftButton()
		{
			ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
			RefreshScenesList();
		}

		static void OnToolbarGUI()
		{

			GUILayout.FlexibleSpace();

			selectedSceneIndex = EditorGUILayout.Popup(selectedSceneIndex,
			                                           scenesPopupDisplay.Select(e => e.popupDisplay).ToArray(),
			                                           GUILayout.Width(100));

			if (GUI.changed && 0 <= selectedSceneIndex && selectedSceneIndex < scenesPopupDisplay.Length)
			{
				if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
				{
					foreach (var scenePath in scenesPath)
					{
						if ((scenePath) == scenesPopupDisplay[selectedSceneIndex].path)
						{
							EditorSceneManager.OpenScene(scenePath);

							break;
						}
					}
				}
			}

		}

		static void RefreshScenesList()
		{
			List<SceneData> toDisplay = new List<SceneData>();

			selectedSceneIndex = -1;

			scenesBuildPath = EditorBuildSettings.scenes.Select(s => s.path).ToArray();

			string[] sceneGuids = AssetDatabase.FindAssets("t:scene", new string[] {"Assets"});
			scenesPath = new string[sceneGuids.Length];

			for (int i = 0; i < scenesPath.Length; ++i)
			{
				scenesPath[i] = AssetDatabase.GUIDToAssetPath(sceneGuids[i]);
			}

			Scene activeScene = SceneManager.GetActiveScene();
			int   usedIds     = scenesBuildPath.Length;

			for (int i = 0; i < scenesBuildPath.Length; ++i)
			{
				string name = GetSceneName(scenesBuildPath[i]);

				if (selectedSceneIndex == -1 && GetSceneName(name) == activeScene.name)
					selectedSceneIndex = i;

				GUIContent content =
					new GUIContent(name, EditorGUIUtility.Load("BuildSettings.Editor.Small") as Texture, "Open scene");

				toDisplay.Add(new SceneData()
				{
					path         = scenesBuildPath[i],
					popupDisplay = content,
				});
			}

			toDisplay.Add(new SceneData()
			{
				path         = "\0",
				popupDisplay = new GUIContent("\0"),
			});
			++usedIds;

			for (int i = 0; i < scenesPath.Length; ++i)
			{
				if (scenesBuildPath.Contains(scenesPath[i]))
					continue;
				string name;

				if (showSceneFolder)
				{
					string folderName = Path.GetFileName(Path.GetDirectoryName(scenesPath[i]));
					name = $"{folderName}/{GetSceneName(scenesPath[i])}";
				}
				else
				{
					name = GetSceneName(scenesPath[i]);
				}

				if (selectedSceneIndex == -1 && name == activeScene.name)
					selectedSceneIndex = usedIds;

				GUIContent content = new GUIContent(name, "Open scene");

				toDisplay.Add(new SceneData()
				{
					path         = scenesPath[i],
					popupDisplay = content,
				});

				++usedIds;
			}

			scenesPopupDisplay = toDisplay.ToArray();
		}

		static string GetSceneName(string path)
		{
			path = path.Replace(".unity", "");

			return Path.GetFileName(path);
		}

	}

	static class SceneHelper
	{
		static string sceneToOpen;

		public static void StartScene(string sceneName)
		{
			if(EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = false;
			}

			sceneToOpen = sceneName;
			EditorApplication.update += OnUpdate;
		}

		static void OnUpdate()
		{
			if (sceneToOpen == null ||
			    EditorApplication.isPlaying || EditorApplication.isPaused ||
			    EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
			{
				return;
			}

			EditorApplication.update -= OnUpdate;

			if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				// need to get scene via search because the path to the scene
				// file contains the package version so it'll change over time
				string[] guids = AssetDatabase.FindAssets("t:scene " + sceneToOpen, null);
				if (guids.Length == 0)
				{
					Debug.LogWarning("Couldn't find scene file");
				}
				else
				{
					string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
					EditorSceneManager.OpenScene(scenePath);
					EditorApplication.isPlaying = true;
				}
			}
			sceneToOpen = null;
		}
	}
}