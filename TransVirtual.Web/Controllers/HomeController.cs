using System.Collections.Specialized;
using System.Web.Mvc;
using TransVirtual.DijkstraAlgorithm;
using TransVirtual.DijkstraAlgorithm.Helpers;
using TransVirtual.DijkstraAlgorithm.Models;

namespace TransVirtual.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly Graph g;

        public HomeController()
        {
            g = CreateGraph();
        }

        public ActionResult Index()
        {
            ShortestPathData model = null;

            if (HttpContext.Request.HttpMethod == "POST")
            {
                var validationResults = ValidateForm(HttpContext.Request.Form);
                if (validationResults != null)
                    ViewBag.Error = validationResults;
                else
                {
                    var from = HttpContext.Request.Form["From"].ToUpper()[0];
                    var to = HttpContext.Request.Form["To"].ToUpper()[0];
                    var algorithm = new Algorithm(g);
                    model = algorithm.GetShortestPath(from, to);
                }
            }

            return View(model);
        }

        private string ValidateForm(NameValueCollection form)
        {
            var from = form["From"].ToUpper();
            var to = form["To"].ToUpper();

            if (from.Length != 1 || !g.HasNode(from[0]))
                return "From should be a character from A to I";
            if (to.Length != 1 || !g.HasNode(to[0]))
                return "To should be a character from A to I";
            else
                return null;
        }

        private Graph CreateGraph()
        {
            var g = new Graph();
            g.AddNodes('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I');

            g.AddEdges('A', 'B', 4, 'C', 6);
            g.AddEdges('B', 'A', 4, 'F', 2);
            g.AddEdges('C', 'A', 6, 'D', 8);
            g.AddEdges('D', 'C', 8, 'E', 4, 'G', 1);
            g.AddEdges('E', 'B', 2, 'D', 4, 'F', 3, 'I', 8);
            g.AddEdges('F', 'B', 2, 'E', 3, 'G', 4, 'H', 6);
            g.AddEdges('G', 'D', 1, 'F', 4, 'H', 5, 'I', 5);
            g.AddEdges('H', 'F', 6, 'G', 5);
            g.AddEdges('I', 'E', 8, 'G', 5);

            return g;
        }
    }
}