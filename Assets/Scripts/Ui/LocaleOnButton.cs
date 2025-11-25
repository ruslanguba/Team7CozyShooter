using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class LocaleOnButton : MonoBehaviour
{
    public void OnClickRu()
    {
        LocalizationSettings.SelectedLocale = Locale.CreateLocale("ru");
    }

    public void OnClickEn()
    {
        LocalizationSettings.SelectedLocale = Locale.CreateLocale("en");
    }
}
