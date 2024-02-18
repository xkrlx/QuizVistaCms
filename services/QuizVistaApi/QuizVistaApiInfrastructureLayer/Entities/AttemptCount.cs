using QuizVistaApiInfrastructureLayer.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiInfrastructureLayer.Entities
{
    [Entity]
    public class AttemptCount
    {
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int AttemptCountNumber { get; set; }
    }
}
