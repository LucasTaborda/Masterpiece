using UnityEngine;

public class Hook : MonoBehaviour
{
    public ScenographyObject self;
    public ScenographyObject senderTarget;
    public ScenographyObject receiverTarget;
    public Sword sword;
    private bool hooked = false;
    private bool delivered = false;
    public GameObject[] hideAfterHooked;
    public GameObject[] hideAfterDelivered;

    void OnTriggerEnter2D(Collider2D collider)
    {
        CheckSender(collider);
        CheckReceiver(collider);
    }

    private void CheckSender(Collider2D collider)
    {
        if(hooked) return;
        if(senderTarget == null) return;
        if(collider.gameObject == senderTarget.gameObject && senderTarget.GetKey() == "SWORD_3" && self.GetKey() == "MOON" ){
            hooked = true;
            senderTarget.SwitchImageToDamaged();
            Game.IsKnifeHooked = true;
            sword.gameObject.SetActive(true);
            sword.isHooked = true;
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_SWORD_TAKE]);
            foreach (GameObject obj in hideAfterHooked)
            {
                obj.SetActive(false);
            }
        }
    }

    private void CheckReceiver(Collider2D collider)
    {
        if(delivered) return;
        if(receiverTarget == null) return;
        // Debug.Log("ENTERED CHECK RECEIVER");
        // Debug.Log("GameObject es igual al receiver: " + (collider.gameObject == receiverTarget.gameObject));
        // Debug.Log("Receiver key: " + receiverTarget.GetKey());
        // Debug.Log("Self key: " + self.GetKey());
        if(collider.gameObject == receiverTarget.gameObject && receiverTarget.GetKey() == "KILLER_3" && self.GetKey() == "MOON" && Game.isActorLiberated){
            delivered = true;
            sword.isHooked = false;
            hooked = false;
            sword.gameObject.SetActive(false);
            receiverTarget.SwitchImageToDamaged();
            Game.actorHasKnife = true;
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_SWORD_GIVE]);
            foreach (GameObject obj in hideAfterDelivered)
            {
                obj.SetActive(false);
            }
        }
    }
}
