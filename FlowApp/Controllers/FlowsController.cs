using Microsoft.AspNetCore.Mvc;

namespace FlowApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlowsController : ControllerBase
    {
        // メモリ上にノードを保持（初期 3 ノード）
        private static readonly List<FlowNode> nodes = new List<FlowNode>
        {
            new FlowNode { Label = "Start",  X = 50,  Y = 50 },
            new FlowNode { Label = "Middle", X = 125, Y = 50 },
            new FlowNode { Label = "End",    X = 200, Y = 50 }
        };

        // GET /flows
        [HttpGet]
        public IEnumerable<FlowNode> Get() => nodes;

        // POST /flows
        [HttpPost]
        public ActionResult<FlowNode> Post([FromBody] FlowNode node)
        {
            if (node == null) return BadRequest();

            nodes.Add(node);  // 新しいノードを追加
            return Ok(node);
        }
    }

    // FlowChart のノード情報
    public class FlowNode
    {
        public string Label { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
