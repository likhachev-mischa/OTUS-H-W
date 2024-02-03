using Declarative;
using Lessons.Entities.Common.Model;

namespace Lessons.Entities.Player
{
    public sealed class PlayerModel : DeclarativeModel
    {
        [Section]
        public Position position;

        [Section]
        public Attack attack;

        [Section]
        public Stats stats;
    }
}