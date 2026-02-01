using UnityEngine;

public class Damager : MonoBehaviour
{
    public ScenographyObject self;
    public ScenographyObject target;
    public Game.RopeDamager type;
    public string selfSkinKey;
    public string targetSkinKey;
    
    private bool isTriggered = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(isTriggered) return;
        if(collider.gameObject == target.gameObject && self.GetKey() == selfSkinKey && target.GetKey() == targetSkinKey){
            Game.DamageRope(type);
            isTriggered = true;
            if(type != Game.RopeDamager.Knife) self.SwitchImageToDamaged();
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_ROPE_RIPPING], 1f);
        }
    }
}
