using Microsoft.AspNetCore.Components;

namespace POC.Frontend.Shared
{
    public partial class GenericList<Titem>
    {
        [EditorRequired, Parameter]
        public List<Titem> MyList { get; set; } = null!;

        [Parameter]
        public RenderFragment? Loading { get; set; }

        [Parameter]
        public RenderFragment? NoRecords { get; set; }

        [EditorRequired]
        [Parameter]
        public RenderFragment Body { get; set; } = null!;
    }
}