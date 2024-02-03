using Declarative;
using Lessons.Entities.Common.Model;

namespace Lessons.Entities.Enemy
{
    public sealed class EnemyModel : DeclarativeModel
    {
        [Section]
        public Position position;

        [Section]
        public Life life;
    }
}