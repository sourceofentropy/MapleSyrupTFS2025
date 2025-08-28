using UnityEngine;

public class NPCPickUpZone : MonoBehaviour
{
    [SerializeField] private AgentTypes.Types agent = AgentTypes.Types.NpcIndividual;

    private void OnTriggerEnter(Collider other)
    {

        UnityEngine.Debug.Log("we have a collision with " + other.gameObject.tag + " check if it matches " + agent);
        UnityEngine.Debug.Log("Is it an npc?");
        string tag = other.gameObject.tag;
        if (other.CompareTag(AgentTypes.GetAgentTypeStringName(agent))) // Check if the correct agent type entered - defaults to NpcIndividual
        {
            UnityEngine.Debug.Log("we have a match with - handle pickup");
            INPCPickUpHandler pickUpHandler = GetComponentInParent<INPCPickUpHandler>();

            if (tag == AgentTypes.GetAgentTypeStringName(AgentTypes.Types.NpcIndividual))
            {
                if (pickUpHandler != null)
                {
                    pickUpHandler.HandlePickup();
                }
            }
        }
    }    
}


