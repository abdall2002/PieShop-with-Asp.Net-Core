using System.Runtime.CompilerServices;
using BethenyPieShop.Models;
using BethenyPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethenyPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public ActionResult List()
        {
            //ViewBag.CurrentCategory = "Cheese cake";
            //return View(_pieRepository.AllPies);
            PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies, "Cheese cake");
            return View(pieListViewModel);

        }

    }
}
