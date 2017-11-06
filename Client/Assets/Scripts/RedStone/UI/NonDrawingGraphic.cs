/// Credit Slipp Douglas Thompson 
/// Sourced from - https://gist.github.com/capnslipp/349c18283f2fea316369

using UnityEngine.UI;
using UnityEngine;

namespace Hotfire.UI
{

	[RequireComponent(typeof(RectTransform))]
	public class NonDrawingGraphic : MaskableGraphic
    {
        public override void SetMaterialDirty() { return; }
        public override void SetVerticesDirty() { return; }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            return;
        }
    }
}