namespace PowBasics.RandomCode;

public static class RndUtils
{
	public static Random Make(int? seed = null) => seed switch
	{
		not null => new Random(seed.Value),
		null => new Random((int)DateTime.Now.Ticks)
	};
}