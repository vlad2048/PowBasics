using System.ComponentModel;
using System.Reflection;

namespace PowBasics.WinForms;

public static class WinFormsUtils
{
	public static bool IsDesignMode => LicenseManager.UsageMode == LicenseUsageMode.Designtime;

	public static void SetDoubleBuffered(this Control ctrl)
	{
		ctrl.GetType().InvokeMember(
			"DoubleBuffered",
			BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
			null,
			ctrl,
			new object[] { true }
		);
	}
}