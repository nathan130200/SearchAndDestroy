rule("[MAPA] King's Row")
{
	event
	{
		Ongoing - Global;
	}

	conditions
	{
		Or(Compare(Current Map, ==, Map(King's Row)), Compare(Current Map, ==, Map(King's Row Winter))) == True;
	}

	actions
	{
		Set Global Variable(_Objective1, Vector(-54.328, 1.491, -24.939));
		Set Global Variable(_Objective2, Vector(-36.740, 3.550, -48.412));
		Modify Global Variable(_attackersSpawnGroup, Append To Array, Vector(-96.110, 2.489, -3.309));
		Modify Global Variable(_attackersSpawnGroup, Append To Array, Vector(-89.859, 2.467, -1.853));
		Modify Global Variable(_attackersSpawnGroup, Append To Array, Vector(-86.090, 2.368, 4.206));
		Modify Global Variable(_attackersSpawnGroup, Append To Array, Vector(-92.360, 2.688, 10.194));
		Modify Global Variable(_attackersSpawnGroup, Append To Array, Vector(-94.584, 2.618, 5.775));
		Modify Global Variable(_defendersSpawnGroup, Append To Array, Vector(-34.366, 1.613, -22.229));
		Modify Global Variable(_defendersSpawnGroup, Append To Array, Vector(-39.431, 1.607, -28.106));
		Modify Global Variable(_defendersSpawnGroup, Append To Array, Vector(-47.649, 1.513, -24.619));
		Modify Global Variable(_defendersSpawnGroup, Append To Array, Vector(-44.564, 3.551, -50.721));
		Modify Global Variable(_defendersSpawnGroup, Append To Array, Vector(-58.255, 1.493, -25.231));
	}
}
