using System.Collections.Generic;

[System.Serializable]
public class ResponseStep {
    public string stepNumber;
    public string title;
    public string phase;
    public List<string> checklistItems;
}

public static class TyphoonData {
    public static List<ResponseStep> Steps = new List<ResponseStep> {
        new ResponseStep {
            stepNumber = "01",
            title = "Secure the Area",
            phase = "BEFORE",
            checklistItems = new List<string> {
                "Reinforce windows and doors with boards or tape",
                "Store loose outdoor items (furniture, pots)",
                "Clear drainage canals near the property",
                "Turn off gas valves and unplug appliances"
            }
        },
        new ResponseStep {
            stepNumber = "02",
            title = "Prepare Emergency Kit",
            phase = "BEFORE",
            checklistItems = new List<string> {
                "3-day food and water supply (4L/person/day)",
                "First aid kit, flashlight, extra batteries",
                "Important documents in waterproof pouch",
                "Cash, phone charger, and power bank"
            }
        },
        new ResponseStep {
            stepNumber = "03",
            title = "Stay Informed",
            phase = "DURING",
            checklistItems = new List<string> {
                "Monitor PAGASA bulletins every 3-6 hours",
                "Follow NDRRMC and LGU announcements",
                "Avoid spreading unverified information",
                "Stay indoors, away from windows"
            }
        },
        new ResponseStep {
            stepNumber = "04",
            title = "Evacuate Safely",
            phase = "AFTER",
            checklistItems = new List<string> {
                "Use designated evacuation routes only",
                "Assist elderly, children, and PWDs",
                "Report to barangay evacuation center",
                "Wait for official all-clear signal"
            }
        }
    };
}