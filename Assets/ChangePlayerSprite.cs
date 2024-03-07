using UnityEngine;
using UnityEngine.UI;

public class SavePlayerSpritePath : MonoBehaviour
{
    [SerializeField] private Button button; // Кнопка для вибору зображення

    void Awake()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    // Викликається при натисканні на кнопку
    public void OnButtonClick()
    {
        // Відкрити галерею для вибору зображення
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Збереження шляху до вибраного зображення в PlayerPrefs
                PlayerPrefs.SetString("PlayerSpritePath", path);
            }
        }, "Select a PNG image", "image/png");

        // Перевірити дозвіл на доступ до галереї
        if (permission == NativeGallery.Permission.Granted)
        {
            // Галерея доступна
        }
        else
        {
            // Дозвіл не наданий
            Debug.LogError("Gallery permission denied");
        }
    }
}