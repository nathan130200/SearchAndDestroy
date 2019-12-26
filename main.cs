variables
{
	global:
		0: _mercyRessurectLimit
		1: _soldierHealingPointLimit
		2: _torbjornTurretLimit
		3: _widowmakerPoisonLimit
		4: _attackersSpawnGroup
		5: _defendersSpawnGroup
		6: _Objective1
		7: _Objective2
		8: _Objective1_Entities
		9: _Objective2_Entities
		10: IsObjective1Enabled
		11: IsObjective2Enabled

	player:
		0: _currentAbilityValue
}

rule("[GLOBAL] Configurar variáveis iniciais.")
{
	event
	{
		Ongoing - Global;
	}

	actions
	{
		Set Global Variable(_mercyRessurectLimit, 1);
		Set Global Variable(_soldierHealingPointLimit, 1);
		Set Global Variable(_torbjornTurretLimit, 1);
		Set Global Variable(_widowmakerPoisonLimit, 1);
		Set Global Variable(_attackersSpawnGroup, Empty Array);
		Set Global Variable(_defendersSpawnGroup, Empty Array);
		Set Global Variable(_Objective1, Vector(0, 0, 0));
		Set Global Variable(_Objective2, Vector(0, 0, 0));
		Set Global Variable(IsObjective1Enabled, True);
		Set Global Variable(IsObjective2Enabled, True);
	}
}

rule("[GLOBAL] Configurar objetivo 1.")
{
	event
	{
		Ongoing - Global;
	}

	actions
	{
		Create Icon(Filtered Array(All Players(All Teams), Global Variable(_Objective1)), Global Variable(_Objective1), Poison,
			Visible To and Position, Lime Green, True);
		Set Global Variable At Index(_Objective1_Entities, 0, Last Created Entity);
		Create Effect(All Players(All Teams), Sparkles, Lime Green, Global Variable(_Objective1), 10, Visible To Position and Radius);
		Set Global Variable At Index(_Objective1_Entities, 1, Last Created Entity);
		Create Effect(All Players(All Teams), Light Shaft, Red, Subtract(Global Variable(_Objective1), Vector(0, 2, 0)), 5,
			Visible To Position and Radius);
		Set Global Variable At Index(_Objective1_Entities, 2, Last Created Entity);
	}
}

rule("[GLOBAL] Configurar objetivo 2.")
{
	event
	{
		Ongoing - Global;
	}

	actions
	{
		Create Icon(Filtered Array(All Players(All Teams), Global Variable(_Objective2)), Global Variable(_Objective2), Poison 2,
			Visible To and Position, Purple, True);
		Set Global Variable At Index(_Objective2_Entities, 0, Last Created Entity);
		Create Effect(All Players(All Teams), Sparkles, Purple, Global Variable(_Objective2), 10, Visible To Position and Radius);
		Set Global Variable At Index(_Objective2_Entities, 1, Last Created Entity);
		Create Effect(All Players(All Teams), Light Shaft, Red, Subtract(Global Variable(_Objective2), Vector(0, 2, 0)), 5,
			Visible To Position and Radius);
		Set Global Variable At Index(_Objective2_Entities, 2, Last Created Entity);
	}
}

rule("[PLAYER] Definir ponto de ressurgimento aleatório.")
{
	event
	{
		Ongoing - Each Player;
		All;
		All;
	}

	conditions
	{
		Has Spawned(Event Player) == True;
	}

	actions
	{
		Skip If(Compare(Team Of(Event Player), ==, Team 2), 2);
		Teleport(Event Player, Random Value In Array(Global Variable(_attackersSpawnGroup)));
		Skip(1);
		Teleport(Event Player, Random Value In Array(Global Variable(_defendersSpawnGroup)));
	}
}

rule("[PLAYER/TORBJORN] Usar gadget.")
{
	event
	{
		Ongoing - Each Player;
		All;
		Torbjörn;
	}

	conditions
	{
		Is Button Held(Event Player, Ability 1) == True;
		Player Variable(Event Player, _currentAbilityValue) <= Global Variable(_torbjornTurretLimit);
	}

	actions
	{
		Small Message(Event Player, Custom String("{0} Gadget utilizado!", Icon String(Recycle), Null, Null));
		Modify Player Variable(Event Player, _currentAbilityValue, Add, 1);
		Wait(0.250, Ignore Condition);
		Set Ability 1 Enabled(Event Player, False);
	}
}

rule("[PLAYER/SOLDADO 76] Usar gadget.")
{
	event
	{
		Ongoing - Each Player;
		All;
		Soldier: 76;
	}

	conditions
	{
		Is Button Held(Event Player, Ability 2) == True;
		Player Variable(Event Player, _currentAbilityValue) < Global Variable(_soldierHealingPointLimit);
	}

	actions
	{
		Small Message(Event Player, Custom String("{0} Gadget utilizado!", Icon String(Recycle), Null, Null));
		Modify Player Variable(Event Player, _currentAbilityValue, Add, 1);
		Wait(0.250, Ignore Condition);
		Set Ability 2 Enabled(Event Player, False);
	}
}

rule("[PLAYER/WIDOWMAKER] Usar gadget.")
{
	event
	{
		Ongoing - Each Player;
		All;
		Widowmaker;
	}

	conditions
	{
		Is Button Held(Event Player, Ability 2) == True;
		Player Variable(Event Player, _currentAbilityValue) < Global Variable(_widowmakerPoisonLimit);
	}

	actions
	{
		Small Message(Event Player, Custom String("{0} Gadget utilizado!", Icon String(Recycle), Null, Null));
		Modify Player Variable(Event Player, _currentAbilityValue, Add, 1);
		Wait(0.250, Ignore Condition);
		Set Ability 2 Enabled(Event Player, False);
	}
}

rule("[PLAYER/MERCY] Usar gadget.")
{
	event
	{
		Ongoing - Each Player;
		All;
		Mercy;
	}

	conditions
	{
		Is Using Ability 2(Event Player) == True;
		Player Variable(Event Player, _currentAbilityValue) < Global Variable(_mercyRessurectLimit);
	}

	actions
	{
		Wait(1.250, Abort When False);
		Modify Player Variable(Event Player, _currentAbilityValue, Add, 1);
		Small Message(Event Player, Custom String("{0} Gadget utilizado!", Icon String(Recycle), Null, Null));
		Wait(0.750, Ignore Condition);
		Set Ability 2 Enabled(Event Player, False);
	}
}
