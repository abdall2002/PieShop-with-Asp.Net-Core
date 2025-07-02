using BethenyPieShop.Models;
using Microsoft.AspNetCore.Components;

namespace BethenyPieShop.App.Pages
{
    public partial class PieCard
    {
        [Parameter]
        public Pie? Pie { get; set; }
    }
}
