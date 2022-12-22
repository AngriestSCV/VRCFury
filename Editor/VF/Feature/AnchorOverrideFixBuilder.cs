using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VF.Builder;
using VF.Feature.Base;
using VF.Inspector;
using VF.Model.Feature;

namespace VF.Feature {
    public class AnchorOverrideFixBuilder : FeatureBuilder<AnchorOverrideFix2> {
        [FeatureBuilderAction(FeatureOrder.AnchorOverrideFix)]
        public void Apply() {
            var animator = avatarObject.GetComponent<Animator>();
            if (!animator) return;
            var root = VRCFArmatureUtils.FindBoneOnArmature(avatarObject, HumanBodyBones.Chest);
            if (!root) root = VRCFArmatureUtils.FindBoneOnArmature(avatarObject, HumanBodyBones.Hips);
            if (!root) return;
            foreach (var skin in avatarObject.GetComponentsInChildren<SkinnedMeshRenderer>(true)) {
                skin.probeAnchor = root.transform;
            }
            foreach (var skin in avatarObject.GetComponentsInChildren<MeshRenderer>(true)) {
                skin.probeAnchor = root.transform;
            }
        }
        
        public override bool AvailableOnProps() {
            return false;
        }
        
        public override string GetEditorTitle() {
            return "Anchor Override Fix";
        }
        
        public override VisualElement CreateEditor(SerializedProperty prop) {
            var content = new VisualElement();
            content.Add(VRCFuryEditorUtils.Info(
                "This feature will set the anchor override for every mesh on your avatar to your chest. " +
                "This will prevent different meshes from being lit differently on your body."));
            return content;
        }
    }
}
