using ThunderRoad;

namespace Invisibility
{
    public class InvisibilitySpell : SpellCastCharge
    {
        public override void Fire(bool active)
        {
            base.Fire(active);
            if(active)
            {
                spellCaster.mana.creature.hidden = !spellCaster.mana.creature.hidden;
                foreach(Creature.RendererData renderer in spellCaster.mana.creature.renderers)
                {
                    if (renderer.splitRenderer != null) renderer.splitRenderer.enabled = !spellCaster.mana.creature.hidden;
                    else if (renderer.renderer != null) renderer.renderer.enabled = !spellCaster.mana.creature.hidden;
                }
                spellCaster.mana.creature.HideItemsInHolders(spellCaster.mana.creature.hidden);
                if (spellCaster.mana.creature.brain.instance.GetModule<BrainModuleSightable>(false) is BrainModuleSightable sight)
                {
                    sight.sightDetectionMaxDistance = spellCaster.mana.creature.hidden ? 0 : 20;
                    sight.sightMaxDistance = spellCaster.mana.creature.hidden ? 5 : 30;
                }
                foreach(Creature creature in Creature.allActive)
                {
                    if(creature.brain.currentTarget == spellCaster.mana.creature && spellCaster.mana.creature.hidden)
                    {
                        creature.brain.currentTarget = null;
                        creature.brain.SetState(Brain.State.Investigate);
                    }
                }
            }
        }
    }
}
