using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:84110df4-a830-4df0-8bad-504ef83a1841
	public partial class UISelectSkill
	{
		public const string Name = "UISelectSkill";
		
		
		private UISelectSkillData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public UISelectSkillData Data
		{
			get
			{
				return mData;
			}
		}
		
		UISelectSkillData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UISelectSkillData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
