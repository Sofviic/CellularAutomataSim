using System.Numerics;

namespace CASim {
	public enum Pid {
		Sand,
		Water,
	}
	public class Particle {
		readonly Pid id;
		(int, int) pos;
		Vector2 velocity;
		public Particle(Pid id, (int, int) pos) : this(id, pos, Vector2.Zero) { }
		public Particle(Pid id, (int, int) pos, Vector2 velocity){
			this.id = id;
			this.pos = pos;
			this.velocity = velocity;
		}
	}
}
