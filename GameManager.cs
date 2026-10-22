using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Otomatis jalan di scene kosong: buat GameManager, lantai, dan pemain.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap()
    {
        var _ = Instance;
        if (FindObjectOfType<PlayerController>() == null)
        {
            var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground"; ground.transform.localScale = new Vector3(10, 1, 10);

            var p = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            p.name = "Player"; p.transform.position = new Vector3(0, 1.1f, 0);
            Destroy(p.GetComponent<CapsuleCollider>());
            p.AddComponent<CharacterController>();
            p.AddComponent<PlayerController>();

            var cam = Camera.main;
            if (cam != null) { cam.transform.position = new Vector3(0, 6, -9); cam.transform.rotation = Quaternion.Euler(30, 0, 0); }
        }
    }

    void Start()
    {
        CharacterManager.Instance.GetCurrent();
        SaveManager.Instance.LoadGame();
        Debug.Log("Aether Impact: Realm of Elements - Siap Dimainkan!");
        Debug.Log("WASD=Gerak | Shift=Lari | Spasi=Lompat/Melayang | 1-4=Ganti Karakter | Klik=Serang | E=Skill | Q=Burst | T=Training | F5=Save");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5)) SaveManager.Instance.SaveGame();
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    void OnGUI()
    {
        var cm = CharacterManager.Instance;
        for (int i = 0; i < cm.partyMembers.Count; i++)
        {
            var c = cm.partyMembers[i];
            GUI.Label(new Rect(10, 10 + i * 22, 520, 22),
                (c.isActive ? "> " : "  ") + "[" + (i + 1) + "] " + c.data.characterName + " (" + c.data.element + ") Lv." + c.level +
                "  HP " + c.currentHP.ToString("0") + "/" + c.maxHP.ToString("0") + "  Energi " + c.energy.ToString("0"));
        }
        if (LevelSystem.Instance.showTrainingNotification)
            GUI.Label(new Rect(10, 120, 400, 22), "Training Deluxe Mode: " + (LevelSystem.Instance.trainingDeluxeMode ? "ON" : "OFF"));
    }
}
