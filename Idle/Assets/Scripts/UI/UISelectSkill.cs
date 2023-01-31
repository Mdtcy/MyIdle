using DefaultNamespace.Events;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Example
{
	public class UISelectSkillData : UIPanelData
	{
	}
	public partial class UISelectSkill : UIPanel
	{
		[SerializeField]
		private Button btn;


		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UISelectSkillData ?? new UISelectSkillData();
			// please add init code here
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
		}

		protected override void OnHide()
		{
			TypeEventSystem.Global.Send<WaveFinishedEvent>();
		}

		protected override void OnClose()
		{
		}

		private void Awake()
		{
			btn.onClick.AddListener(CloseSelf);
		}
	}
}
