//Author: João Azuaga

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snobfox {
	public interface ICollisionSource {
		CollisionResult Collide(object source, object parameters);
	}
}
