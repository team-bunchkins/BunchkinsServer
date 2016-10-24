namespace Bunchkins.Migrations
{
    using Domain.Cards.Door.Monsters;
    using Domain.Cards.Door.Spells;
    using Domain.Cards.Door.Spells.Curses;
    using Domain.Cards.Treasure.Equipment;
    using Domain.Cards.Treasure.Spells;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bunchkins.Infrastructure.BunchkinsDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Bunchkins.Infrastructure.BunchkinsDataContext context)
        {
            if(context.Cards.Count() == 0)
            {
                //Door Cards
                context.Cards.Add(new BuffMonsterSpellDoorCard { Description = "Play During Combat. +5 to Monster Level. Active Player Gains 1 Additional Treasure Upon Monster Defeat", FlavorText = "You'll like him when he's angry. If you aren't the one fighting.", Name = "Enraged Monster", CombatPower = 5, TreasureChange = 1, Type = "CombatSpell", PictureUrl = "combatspell/Enraged.jpg" });
                context.Cards.Add(new BuffMonsterSpellDoorCard { Description = "Play During Combat. +5 to Monster Level. Active Player Gains 1 Additional Treasure Upon Monster Defeat", FlavorText = "The square root of pie is, IT'S EATING ME!!!!", Name = "Intelligent Monster", CombatPower = 5, TreasureChange = 1, Type = "CombatSpell", PictureUrl = "combatspell/Intelligent.jpg" });
                context.Cards.Add(new BuffMonsterSpellDoorCard { Description = "Play During Combat. +10 to Monster Level. Active Player Gains 2 Additional Treasure Upon Monster Defeat", FlavorText = "Her fingers will blot out the sun.", Name = "Humongous Monster", CombatPower = 10, TreasureChange = 2, Type = "CombatSpell", PictureUrl = "combatspell/Humongous.jpg" });
                context.Cards.Add(new BuffMonsterSpellDoorCard { Description = "Play During Combat. +10 to Monster Level. Active Player Gains 2 Additional Treasure Upon Monster Defeat", FlavorText = "The oldest of schools.", Name = "Ancient Monster", CombatPower = 10, TreasureChange = 2, Type = "CombatSpell", PictureUrl = "combatspell/Ancient.jpg" });
                context.Cards.Add(new BuffMonsterSpellDoorCard { Description = "Play During Combat. -5 to Monster Level. Active Player Gains 1 Less Treasure Upon Monster Defeat", FlavorText = "TOO.....CUTE....Gonna....DIE.", Name = "Baby Monster", CombatPower = -5, TreasureChange = -1, Type = "CombatSpell", PictureUrl = "combatspell/Baby.jpg" });
                
                context.Cards.Add(new AddMonsterSpellCard { Description = "Add a random monster to this Combat Phase.", FlavorText = "This room looks fun. OH GOD WHAT'S THAT?!", Name = "Wandering Monster", Type = "CombatSpell", PictureUrl = "combatspell/WanderingMonster2.jpg" });
                context.Cards.Add(new AddMonsterSpellCard { Description = "Add a random monster to this Combat Phase.", FlavorText = "A NEW CHALLENGER APPEARS!", Name = "Arcade Fight", Type = "CombatSpell", PictureUrl = "combatspell/WanderingMonster1.jpg" });

                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Bad Stuff: Lose Headgear", FlavorText = "The biggest and most hairy of feet.", Name = "Bigfoot", Level = 12, TreasureGain = 3, LevelGain = 1, Slot = "Headgear", Type = "Monster", PictureUrl = "monster/Bigfoot.jpg" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Bad Stuff: Lose Footgear", FlavorText = "I wont ask how you got them. But you do have them.", Name = "Crabs", Level = 1, TreasureGain = 1, LevelGain = 1, Slot = "Footgear", Type = "Monster", PictureUrl = "monster/Crabs.jpg" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Bad Stuff: Lose Headgear", FlavorText = "You should have ducked when the egg hatched.", Name = "Face Sucker", Level = 8, TreasureGain = 2, LevelGain = 1, Slot = "Headgear", Type = "Monster", PictureUrl = "monster/FaceSucker.jpg" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Bad Stuff: Lose Footgear", FlavorText = "Similar to the jello your aunt makes. Only less deadly.", Name = "Drooling Slime", Level = 1, TreasureGain = 1, LevelGain = 1, Slot = "Footgear", Type = "Monster", PictureUrl = "monster/DroolingSlime.jpg" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Bad Stuff: Lose Headgear", FlavorText = "Never try to take away his internet. Unless you enjoy shattered eardrums.", Name = "Shrieking Geek", Level = 6, TreasureGain = 2, LevelGain = 1, Slot = "Headgear", Type = "Monster", PictureUrl = "monster/ShriekingGeek.jpg" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Bad Stuff: Lose Footgear", FlavorText = "Gotta go to work. Work all day. Won't stop until they have underpants, HEY.", Name = "Underpants Gnomes", Level = 4, TreasureGain = 2, LevelGain = 1, Slot = "Footgear", Type = "Monster", PictureUrl = "monster/UnderpantsGnomes.jpg" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Bad Stuff: Lose Footgear", FlavorText = "Oh god, it's turbo all over again.", Name = "Snails on speed", Level = 4, TreasureGain = 1, LevelGain = 1, Slot = "Footgear", Type = "Monster", PictureUrl = "monster/SnailsOnSpeed.jpg" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Bad Stuff: Lose Armor", FlavorText = "She doesn't like being put inside the puk�ball.", Name = "Pukachu", Level = 6, TreasureGain = 2, LevelGain = 1, Slot = "Armor", Type = "Monster", PictureUrl = "monster/Pukachu.jpg" });

                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 1 Levels", FlavorText = "It came from THE TAP!!!!", Name = "Bottled Terror Water", Level = 1, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster", PictureUrl = "monster/BottledTerrorWater.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 1 Levels", FlavorText = "Try not to fall over.", Name = "A Light Breeze", Level = 1, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster", PictureUrl = "monster/ALightBreeze.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 1 Levels", FlavorText = "Warning: Do Not Place Over Head.", Name = "Trash Bag Of Death", Level = 1, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster", PictureUrl = "monster/TrashBagOfDeath.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 1 Levels", FlavorText = "Just give up the candy before we all get killed.", Name = "Kindergartener", Level = 1, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster", PictureUrl = "monster/Kindergartener.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 1 Levels", FlavorText = "It's only weakness is a tissue.", Name = "Mutant Snot", Level = 1, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster", PictureUrl = "monster/MutantSnot.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 1 Levels", FlavorText = "Seriously, just take them off your face.", Name = "Broken Glasses", Level = 1, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster", PictureUrl = "monster/BrokenGlasses.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 1 Levels", FlavorText = "You know he's coming when you hear the snaps.", Name = "Cole", Level = 1, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster", PictureUrl = "monster/Cole.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 3 Levels", FlavorText = "He wants to suck your blood per se.", Name = "Wannabe Vampire", Level = 12, LevelGain = 1, TreasureGain = 4, LevelsDecreased = 3, Type = "Monster", PictureUrl = "monster/WannabeVampire.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 2 Levels", FlavorText = "At least you'll die at the hands of a fantastic kisser.", Name = "Tongue Demon", Level = 12, LevelGain = 1, TreasureGain = 3, LevelsDecreased = 2, Type = "Monster", PictureUrl = "monster/TongueDemon.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 3 Levels", FlavorText = "Beware falling snot.", Name = "Floating Nose", Level = 10, LevelGain = 1, TreasureGain = 3, LevelsDecreased = 3, Type = "Monster", PictureUrl = "monster/FloatingNose.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 3 Levels", FlavorText = "Not your average wedding venue. OR IS IT?!?!", Name = "Gazebo", Level = 8, LevelGain = 1, TreasureGain = 2, LevelsDecreased = 3, Type = "Monster", PictureUrl = "monster/Gazebo.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 2 Levels", FlavorText = "It's a duck...wait it's a seal...either way it's coming straight for you.", Name = "Platycore", Level = 6, LevelGain = 1, TreasureGain = 2, LevelsDecreased = 2, Type = "Monster", PictureUrl = "monster/Platycore.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 2 Levels", FlavorText = "Don't harp on the details too much, they just want your flesh.", Name = "Harpies", Level = 4, LevelGain = 1, TreasureGain = 2, LevelsDecreased = 2, Type = "Monster", PictureUrl = "monster/Harpies.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 2 Levels", FlavorText = "Look at my horse. My horse is amaz......oh it's dead.", Name = "Undead Horse", Level = 4, LevelGain = 1, TreasureGain = 2, LevelsDecreased = 2, Type = "Monster", PictureUrl = "monster/UndeadHorse.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 2 Levels", FlavorText = "He just wants your love.", Name = "Pit Bull", Level = 2, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 2, Type = "Monster", PictureUrl = "monster/PitBull.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 1 Levels", FlavorText = "The Kernel couldn't catch this one for proccessing.", Name = "Large Angry Chicken", Level = 2, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster", PictureUrl = "monster/LargeAngryChicken.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 2 Levels", FlavorText = "Spooky scary skeletons send shivers down your spine.", Name = "Mr.Bones", Level = 2, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 2, Type = "Monster", PictureUrl = "monster/MrBones.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 2 Levels", FlavorText = "The French say they taste like chicken.", Name = "Flying Frogs", Level = 2, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 2, Type = "Monster", PictureUrl = "monster/FlyingFrogs.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 1 Levels", FlavorText = "He took his mom to prom.", Name = "Lame Goblin", Level = 1, LevelGain = 1, TreasureGain =1, LevelsDecreased = 1, Type = "Monster", PictureUrl = "monster/LameGoblin.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 1 Levels", FlavorText = "Enjoys hanging out by The Gap and putting Gaps in shoppers' heads.", Name = "Maul Rat", Level = 1, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster", PictureUrl = "monster/MaulRat.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 3 Levels", FlavorText = "Beware her drones.", Name = "Amazon", Level = 8, LevelGain = 1, TreasureGain = 2, LevelsDecreased = 3, Type = "Monster", PictureUrl = "monster/Amazon.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose 1 Levels", FlavorText = "Come on, can't you photosypmathize with it?", Name = "Potted Plant", Level = 1, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster", PictureUrl = "monster/PottedPlant.jpg" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Bad Stuff: Lose All Levels", FlavorText = "Their dream of flying came true after their attempt failed.", Name = "The Wight Brothers", Level = 16, LevelGain = 2, TreasureGain = 4, LevelsDecreased = 10, Type = "Monster", PictureUrl = "monster/TheWightBrothers.jpg" });

                context.Cards.Add(new DeathMonsterCard { Description = "Minimum Pursuit Level: 6, Bad Stuff: Die for being delicious.", FlavorText = "Radiation levels OVER 9000!!!", Name = "Plutonium Dragon", Level = 20, TreasureGain = 5, LevelGain = 2, MinPursuitLevel = 6, Type = "Monster", PictureUrl = "monster/PlutoniumDragon.jpg" });
                context.Cards.Add(new DeathMonsterCard { Description = "Minimum Pursuit Level: 4, Bad Stuff: Die a terrible death.", FlavorText = "The first one to walk like an Egyptian.", Name = "King Tut", Level = 16, TreasureGain = 4, LevelGain = 2, MinPursuitLevel = 4, Type = "Monster", PictureUrl = "monster/KingTut.jpg" });
                context.Cards.Add(new DeathMonsterCard { Description = "Minimum Pursuit Level: 4, Bad Stuff: Flattened to death.", FlavorText = "Beware low flying mammals.", Name = "Hippogriff", Level = 16, TreasureGain = 4, LevelGain = 2, MinPursuitLevel = 4, Type = "Monster", PictureUrl = "monster/Hippogriff.jpg" });
                context.Cards.Add(new DeathMonsterCard { Description = "Bad Stuff: Crushed to death by the number 5.", FlavorText = "You better roll a 20. Oh wait....", Name = "Gelatinous Octahedron", Level = 2, TreasureGain = 1, LevelGain = 2, Type = "Monster", PictureUrl = "monster/GelatinousOctahedron.jpg" });
                context.Cards.Add(new DeathMonsterCard { Description = "Bad Stuff: Death by injunction.", FlavorText = "Your life is overruled.", Name = "Lawyers", Level = 6, TreasureGain = 2, LevelGain = 1, Type = "Monster", PictureUrl = "monster/Lawyers.jpg" });
                context.Cards.Add(new DeathMonsterCard { Description = "Bad Stuff: Die with plenty of warnings.", FlavorText = "THAT'S ALOT OF ORCS!!!", Name = "3,872 Orcs", Level = 10, TreasureGain = 3, LevelGain = 1, Type = "Monster", PictureUrl = "monster/3872Orcs.jpg" });
                context.Cards.Add(new DeathMonsterCard { Description = "Bad Stuff: Die for your sins.", FlavorText = "There are no words. Only screams.", Name = "Unspeakably Awful Indescribable Horror", Level = 14, TreasureGain = 4, LevelGain = 1, Type = "Monster", PictureUrl = "monster/UnspeakablyAwfulIndescribableHorror.jpg" });
                context.Cards.Add(new DeathMonsterCard { Description = "Bad Stuff: Die dude.", FlavorText = "He too is a fan of Pineapple Express.", Name = "Stoned Golem", Level = 14, TreasureGain = 4, LevelGain = 1, Type = "Monster", PictureUrl = "monster/StoneGolem.jpg" });
                context.Cards.Add(new DeathMonsterCard { Description = "Bad Stuff: Death by suction cups.", FlavorText = "Poorly dubbed sushi awaits your demise.", Name = "Squidzilla", Level = 18, TreasureGain = 4, LevelGain = 2, Type = "Monster", PictureUrl = "monster/Squidzilla.jpg" });
                context.Cards.Add(new DeathMonsterCard { Description = "Minimum Pursuit Level: 5, Bad Stuff: Just Die.", FlavorText = "You should probably let him pass.", Name = "Bullrog", Level = 18, TreasureGain = 5, LevelGain = 2, MinPursuitLevel = 5, Type = "Monster", PictureUrl = "monster/Bullrog.jpg" });

                context.Cards.Add(new LoseAllEquipCurseCard { Description = "Target Player Loses all Equipment", FlavorText = "Either way, your friends still think your pretty.", Name = "Change Sex", Type = "Curse", PictureUrl = "curse/ChangeSex.jpg" });
                context.Cards.Add(new LoseAllEquipCurseCard { Description = "Target Player Loses all Equipment", FlavorText = "The Man has come for what you owe.", Name = "Income Tax", Type = "Curse", PictureUrl = "curse/IncomeTax.jpg" });

                context.Cards.Add(new LoseEquipCurseCard { Description = "Target Loses Footgear", FlavorText = "It's not the good kind of sticky.", Name = "Tar Trap", Slot = "Footgear", Type = "Curse", PictureUrl = "curse/LoseFootgear.jpg" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "Target Loses Headgear", FlavorText = "Standing under it with a metal helmet wasn't a good idea.", Name = "Giant Magnet", Slot = "Headgear", Type = "Curse", PictureUrl = "curse/LoseHeadgear.jpg" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "Target Loses Headgear", FlavorText = "You should use better dandruff shampoo to keep these away.", Name = "Chicken on your head", Slot = "Headgear", Type = "Curse", PictureUrl = "curse/ChickenOnYourHead.jpg" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "Target Loses Armor", FlavorText = "The quality was not worth the price.", Name = "Knockoff Armor Polish", Slot = "Armor", Type = "Curse", PictureUrl = "curse/LoseArmor.jpg" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "Target Loses Headgear", FlavorText = "Probably some wizards sick joke.", Name = "Flying Hat", Slot = "Headgear", Type = "Curse", PictureUrl = "curse/FlyingHeadgear.jpg" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "Target Loses 1 Hander", FlavorText = "You really don't know how to maintain your weapon do you?", Name = "Bumpy Grind Stone", Slot = "1Hand", Type = "Curse", PictureUrl = "curse/Lose1Hander.jpg" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "Target Loses 2 Hander", FlavorText = "It REALLY wanted your two hander.", Name = "Very Accurate Black Hole", Slot = "2Hands", Type = "Curse", PictureUrl = "curse/Lose2Hander.jpg" });

                context.Cards.Add(new LoseHandCardsCurseCard { Description = "Target Player Loses 2 Cards from Hand", FlavorText = "On the bright side, you can use toast as papertowels.", Name = "Butter Fingers", NumCards = 2 , Type = "Curse", PictureUrl = "curse/LoseTwoCards.jpg" });
                context.Cards.Add(new LoseHandCardsCurseCard { Description = "Target Player Loses 1 Card from Hand", FlavorText = "An Underhanded tactic....but effective.", Name = "Poked In The Eye", NumCards = 1, Type = "Curse", PictureUrl = "curse/LoseTwoCards.jpg" });
                context.Cards.Add(new LoseHandCardsCurseCard { Description = "Target Player Loses 3 Cards from Hand", FlavorText = "Just add water and wait 3 minutes.", Name = "Noodle Brain", NumCards = 3, Type = "Curse", PictureUrl = "curse/LoseTwoCards.jpg" });
                context.Cards.Add(new LoseHandCardsCurseCard { Description = "Target Player Loses 1 Card from Hand", FlavorText = "Is that Big Foot?", Name = "Look Over There", NumCards = 1, Type = "Curse", PictureUrl = "curse/LoseTwoCards.jpg" });

                context.Cards.Add(new LoseLevelCurseCard { Description = "Target Player Loses 2 Levels", FlavorText = "And here you thought it was a cute duck.", Name = "Duck of Doom", Levels = 2, Type = "Curse", PictureUrl = "curse/DuckOfDoom.jpg" });
                context.Cards.Add(new LoseLevelCurseCard { Description = "Target Player Loses 1 Level", FlavorText = "You used to be awsome, but you can't remember how anymore.", Name = "Amnesia", Levels = 1, Type = "Curse", PictureUrl = "curse/LoseLevel1.jpg" });
                context.Cards.Add(new LoseLevelCurseCard { Description = "Target Player Loses 1 Level", FlavorText = "Don't even try to blame it on the dog.", Name = "Shamefull Gas", Levels = 1, Type = "Curse", PictureUrl = "curse/LoseLevel2.jpg" });



                //Treasure Cards
                context.Cards.Add(new LevelUpSpellCard { Description = "Gain 1 Level", FlavorText = "It did it's job.", Name = "Potion of General Studliness", Level = 1, Type = "Spell", PictureUrl = "spell/PotionOfGeneralStudliness.jpg" });
                context.Cards.Add(new LevelUpSpellCard { Description = "Gain 1 Level", FlavorText = "You get a level only because he thinks your mom is hot.", Name = "Whine at the GM", Level = 1, Type = "Spell", PictureUrl = "spell/WhineAtTheGM.jpg" });
                context.Cards.Add(new LevelUpSpellCard { Description = "Gain 1 Level", FlavorText = "Your nerd knowledge is not to be trifled with.", Name = "Invoke Obscure Rules", Level = 1, Type = "Spell", PictureUrl = "spell/InvokeObscureRules.jpg" });
                context.Cards.Add(new LevelUpSpellCard { Description = "Gain 1 Level", FlavorText = "He owed you money.", Name = "Kill That Guy There", Level = 1, Type = "Spell", PictureUrl = "spell/KillTheHireling.jpg" });
                context.Cards.Add(new LevelUpSpellCard { Description = "Gain 1 Level", FlavorText = "Your lust for digging through the remains of your enemys is....outstanding.", Name = "Corpse Harvesting", Level = 1, Type = "Spell", PictureUrl = "spell/MutilateTheBodies.jpg" });
                context.Cards.Add(new LevelUpSpellCard { Description = "Gain 1 Level", FlavorText = "They ruined your picknick. MAKE THEM PAY!", Name = "Boil an Anthill", Level = 1, Type = "Spell", PictureUrl = "spell/BoilAnAnthill.jpg" });
                context.Cards.Add(new LevelUpSpellCard { Description = "Gain 1 Level", FlavorText = "You deserve EVERYTHING!!!", Name = "Check Your Privilege", Level = 1, Type = "Spell", PictureUrl = "spell/1000GoldPieces.jpg" });
                context.Cards.Add(new LevelUpSpellCard { Description = "Gain 1 Level", FlavorText = "The pizza was free, but he doesn't need to know that.", Name = "Bribe GM with food", Level = 1, Type = "Spell", PictureUrl = "spell/BribeGMWithFood.jpg" });
                context.Cards.Add(new LevelUpSpellCard { Description = "Gain 1 Level", FlavorText = "2 + 2 = whatever you want bro.", Name = "Convenient Addition Error", Level = 1, Type = "Spell", PictureUrl = "spell/ConvenientAdditionError.jpg" });

                context.Cards.Add(new EscapeAllSpellCard { Description = "Escape Current Combat. Do Not Gain Monster's Treasure.", FlavorText = "When in doubt, BAIL!!!", Name = "Invisibility Potion", Gold = 200, Type = "CombatSpell", PictureUrl = "combatspell/InvisibilityPotion.jpg" });
                context.Cards.Add(new EscapeAllSpellCard { Description = "Escape Current Combat. Do Not Gain Monster's Treasure.", FlavorText = "Why can't we be friends? Oh, we can.", Name = "Friendship Potion", Gold = 200, Type = "CombatSpell", PictureUrl = "combatspell/FriendshipPotion.jpg" });
                context.Cards.Add(new EscapeAllSpellCard { Description = "Escape Current Combat. Do Not Gain Monster's Treasure.", FlavorText = "From the makers of Instant Coffee and Faster DMV Lines.", Name = "Instant Wall", Gold = 300, Type = "CombatSpell", PictureUrl = "combatspell/InstantWall.jpg" });

                context.Cards.Add(new VanquishMonsterSpellCard { Description = "Destroy Monsters in Current Combat. Active Player Gains Treasures that would be earned.", FlavorText = "That golem looks tough...aaaaannnd its a rooster.", Name = "Pollymorph Potion", Gold = 1300, Type = "CombatSpell", PictureUrl = "combatspell/PollymorphPotion.jpg" });
                context.Cards.Add(new VanquishMonsterSpellCard { Description = "Destroy Monsters in Current Combat. Active Player Gains Treasures that would be earned.", FlavorText = "You get one wish. Alladin was a liar", Name = "Magic Lamp", Gold = 500, Type = "CombatSpell", PictureUrl = "combatspell/MagicLamp.jpg" });

                context.Cards.Add(new BuffPlayerSpellCard { Description = "+4 Combat Power For This Combat Phase", FlavorText = "Somewhere in between Harry Potter and that creepy guy in the park.", Name = "Cloak of Obscurity", CombatPower = 4, Gold = 600, Type = "CombatSpell", PictureUrl = "combatspell/CloakOfObscurity.jpg" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "+3 Combat Power For This Combat Phase", FlavorText = "The highground always wins. Why not make your own.", Name = "Stepladder", CombatPower = 3, Gold = 400, Type = "CombatSpell", PictureUrl = "combatspell/Stepladder.jpg" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "+2 Combat Power For This Combat Phase", FlavorText = "Well...a distraction is always good.", Name = "Singing & Dancing Sword", CombatPower = 2, Gold = 400, Type = "CombatSpell", PictureUrl = "combatspell/SingingAndDancingSword.jpg" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "+3 Combat Power For This Combat Phase", FlavorText = "At least my friends think I'm cool.", Name = "Really Impressive Title", CombatPower = 3, Type = "CombatSpell", PictureUrl = "combatspell/ReallyImpressiveTitle.jpg" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "+3 Combat Power For This Combat Phase", FlavorText = "Just throw it already. It stinks in here.", Name = "Limburger and Anchovy Sandwich", CombatPower = 3, Gold = 400, Type = "CombatSpell", PictureUrl = "combatspell/LimburgerAndAnchovySandwich.jpg" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "+1 Combat Power For This Combat Phase", FlavorText = "You'll never slow dance again, but you can really kill a gnome.", Name = "Spiky Knees", CombatPower = 1, Gold = 200, Type = "CombatSpell", PictureUrl = "combatspell/SpikyKnees.jpg" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "+3 Combat Power For This Combat Phase", FlavorText = "Look. Just put them on and lift that building.", Name = "Pantyhose of Giant Strength", CombatPower = 3, Gold = 600, Type = "CombatSpell", PictureUrl = "combatspell/PantyhoseOfGiantStrength.jpg" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "+5 Combat Power For This Combat Phase", FlavorText = "DnD for life.", Name = "Magic Missile", CombatPower = 5, Gold = 300, Type = "CombatSpell", PictureUrl = "combatspell/MagicMissile.jpg" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "+2 Combat Power For This Combat Phase", FlavorText = "You think your high class with your fancy water, and your right.", Name = "Yuppie Water", CombatPower = 2, Gold = 100, Type = "CombatSpell", PictureUrl = "combatspell/YuppieWater.jpg" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "+3 Combat Power For This Combat Phase", FlavorText = "Don't drop it and get cold feet.", Name = "Freezing Explosive Potion", CombatPower = 3, Gold = 100, Type = "CombatSpell", PictureUrl = "combatspell/FreezingExplosivePotion.jpg" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "+5 Combat Power For This Combat Phase", FlavorText = "No one can resist......THE BALLOON.", Name = "Pretty Balloons", CombatPower = 5, Type = "CombatSpell", PictureUrl = "combatspell/PrettyBalloons.jpg" });

                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "+2 Monster Combat Power For This Combat Phase", FlavorText = "Stay arms length from that monster. Mayber further.", Name = "Halitosis", CombatPower = 2, Gold = 100, Type = "CombatSpell", PictureUrl = "combatspell/PotionOfHalitosis.jpg" });
                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "+3 Monster Combat Power For This Combat Phase", FlavorText = "Here. Try this.", Name = "Flaming Poison Potion", CombatPower = 3, Gold = 100, Type = "CombatSpell", PictureUrl = "combatspell/FlamingPoisonPotion.jpg" });
                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "+3 Monster Combat Power For This Combat Phase", FlavorText = "Swing until you hit somebody. Then swing some more.", Name = "Cotion of Ponfusion", CombatPower = 3, Gold = 100, Type = "CombatSpell", PictureUrl = "combatspell/CotionOfPonfusion.jpg" });
                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "+2 Monster Combat Power For This Combat Phase", FlavorText = "Looks like your friend needs a nap. Help them out.", Name = "Sleep Potion", CombatPower = 2, Gold = 100, Type = "CombatSpell", PictureUrl = "combatspell/SleepPotion.jpg" });
                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "+2 Monster Combat Power For This Combat Phase", FlavorText = "They uhh.....shouldn't try to fight that.", Name = "Potion of Idiotic Bravery", CombatPower = 2, Gold = 100, Type = "CombatSpell", PictureUrl = "combatspell/PotionOfIdioticBravery.jpg" });
                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "+2 Monster Combat Power For This Combat Phase", FlavorText = "Why would you drink this.", Name = "Nasty-Tasting Sports Drink", CombatPower = 2, Gold = 200, Type = "CombatSpell", PictureUrl = "combatspell/NastyTastingSportsDrink.jpg" });

                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Body Armor,  Combat Power Bonus: +1", FlavorText = "Slimy and effective.", Slot = "Armor", Gold = 200, Bonus = 1, Name = "Slimy Armor", Type = "Equipment", PictureUrl = "equipment/SlimyArmor.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Headgear,  Combat Power Bonus: +3", FlavorText = "Read the name.", Slot = "Headgear", Gold = 400, Bonus = 3, Name = "Bad-Ass Bandanna", Type = "Equipment", PictureUrl = "equipment/BadAssBandanna.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Body Armor,  Combat Power Bonus: +3", FlavorText = "You'll like the way you look. I guarantee it.", Slot = "Armor", Gold = 400, Bonus = 3, Name = "Short Wide Armor", Type = "Equipment", PictureUrl = "equipment/ShortWideArmor.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 2 Hands,  Combat Power Bonus: +4", FlavorText = "Well.....it's pretty.", Slot = "2Hands", Gold = 800, Bonus = 4, Name = "Bow With Ribbons", Type = "Equipment", PictureUrl = "equipment/BowWithRibbon.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1 Hand,  Combat Power Bonus: +3", FlavorText = "She is just loaning it to you.", Slot = "1Hand", Gold = 400, Bonus = 3, Name = "Broad Sword", Type = "Equipment", PictureUrl = "equipment/BroadSword.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Footgear,  Combat Power Bonus: +2", FlavorText = "Be glad you aren't on the receiving end of these.", Slot = "Footgear", Gold = 400, Bonus = 2, Name = "Boots of Butt-Kicking", Type = "Equipment", PictureUrl = "equipment/BootsOfButtKicking.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1 Hand,  Combat Power Bonus: +5", FlavorText = "BURN BABY BURN!!!", Slot = "1Hand", Gold = 800, Bonus = 5, Name = "Staff of Napalm", Type = "Equipment", PictureUrl = "equipment/StaffofNaplam.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Headgear,  Combat Power Bonus: +3", FlavorText = "It wouldn't be the same if it wasn't pointy.", Slot = "Headgear", Gold = 400, Bonus = 3, Name = "Pointy Hat of Power", Type = "Equipment", PictureUrl = "equipment/PointyHatofPower.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Headgear,  Combat Power Bonus: +1", FlavorText = "The fact that it blocks your vision is probably helping.", Slot = "Headgear", Gold = 200, Bonus = 1, Name = "Helm of Courage", Type = "Equipment", PictureUrl = "equipment/HelmOfCourage.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Headgear,  Combat Power Bonus: +1", FlavorText = "This helmet just thinks of you as a friend....I swear.", Slot = "Headgear", Gold = 300, Bonus = 1, Name = "Horny Helmet", Type = "Equipment", PictureUrl = "equipment/HornyHelmet.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1 Hand,  Combat Power Bonus: +1", FlavorText = "The stink is it's only real power.", Slot = "1Hand", Gold = 200, Bonus = 1, Name = "Rat On A Stick", Type = "Equipment", PictureUrl = "equipment/RatOnAStick.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1Hand,  Combat Power Bonus: +3", FlavorText = "Place gently in a friend's back.", Slot = "1Hand", Gold = 400, Bonus = 3, Name = "Dagger of Treachery", Type = "Equipment", PictureUrl = "equipment/DaggerofTreachery.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Body Armor,  Combat Power Bonus: +2", FlavorText = "The burn means it's working.", Slot = "Armor", Gold = 400, Bonus = 2, Name = "Flaming Armor", Type = "Equipment", PictureUrl = "equipment/FlamingArmor.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 2 Hands,  Combat Power Bonus: +3", FlavorText = "Get all of the guys and gals with this.", Slot = "2Hands", Gold = 300, Bonus = 3, Name = "Tuba of Charm", Type = "Equipment", PictureUrl = "equipment/TubaofCharm.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Footgear,  Combat Power Bonus: +1", FlavorText = "Run Forest Run.", Slot = "Footgear", Gold = 400, Bonus = 1, Name = "Boots of running REALLY FAST", Type = "Equipment", PictureUrl = "equipment/BootsOfRunningReallyFast.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1 Hand,  Combat Power Bonus: +3", FlavorText = "Make your enemies a delicious topping to any taco.", Slot = "1Hand", Gold = 400, Bonus = 3, Name = "Cheese Grater of Peace", Type = "Equipment", PictureUrl = "equipment/CheeseGraterofPeace.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Body Armor,  Combat Power Bonus: +1", FlavorText = "Your hobbies are......interesting.", Slot = "Armor", Gold = 200, Bonus = 1, Name = "Spandex", Type = "Equipment", PictureUrl = "equipment/Spandex.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1 Hand,  Combat Power Bonus: +2", FlavorText = "Your dad bought you this. Oh wait...", Slot = "1Hand", Gold = 400, Bonus = 2, Name = "Sneaky Bastard Sword", Type = "Equipment", PictureUrl = "equipment/SneakyBastardSword.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 2 Hands,  Combat Power Bonus: +1", FlavorText = "Now you can touch whatever you want.", Slot = "2Hands", Gold = 200, Bonus = 1, Name = "Eleven-Foot Pole", Type = "Equipment", PictureUrl = "equipment/ElevenFootPole.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Footgear,  Combat Power Bonus: +2", FlavorText = "Basically if crocks didn't suck.", Slot = "Footgear", Gold = 700, Bonus = 2, Name = "Sandals of Protection", Type = "Equipment", PictureUrl = "equipment/SandalsofProtection.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: Body Armor,  Combat Power Bonus: +3", FlavorText = "The best protection for any Hobit on the go.", Slot = "Armor", Gold = 600, Bonus = 3, Name = "Mithril Armor", Type = "Equipment", PictureUrl = "equipment/MithrilArmor.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 2 Hands,  Combat Power Bonus: +3", FlavorText = "Throw at those who disobey you.", Slot = "2Hands", Gold = 100, Bonus = 3, Name = "Huge Rock", Type = "Equipment", PictureUrl = "equipment/HugeRock.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 2 Hands,  Combat Power Bonus: +4", FlavorText = "For all your adventuring needs, like opening a can of beans.", Slot = "2Hands", Gold = 600, Bonus = 4, Name = "Swiss Army Polearm", Type = "Equipment", PictureUrl = "equipment/SwissArmyPolearm.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 2 Hands,  Combat Power Bonus: +3", FlavorText = "Great for getting a head in life.", Slot = "2Hands", Gold = 600, Bonus = 3, Name = "Chainsaw of Bloody Dismemberment", Type = "Equipment", PictureUrl = "equipment/ChainsawofBloodyDismemberment.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1 Hand,  Combat Power Bonus: +4", FlavorText = "Always a good thing to be able to defend against GOD.", Slot = "1Hand", Gold = 600, Bonus = 4, Name = "Shield of Ubiquity", Type = "Equipment", PictureUrl = "equipment/ShieldofUbiquity.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1 Hand,  Combat Power Bonus: +3", FlavorText = "It's not cheating just because you can hit them but they can't hit you.", Slot = "1Hand", Gold = 600, Bonus = 3, Name = "Rapier of Unfairness", Type = "Equipment", PictureUrl = "equipment/RapierofUnfairness.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1 Hand,  Combat Power Bonus: +2", FlavorText = "For those who wanted to be a pirate but have dyxlesia.", Slot = "1Hand", Gold = 400, Bonus = 2, Name = "Buckler of Swashing", Type = "Equipment", PictureUrl = "equipment/BucklerofSwashing.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1 Hand,  Combat Power Bonus: +4", FlavorText = "Any other mace would have been dull.", Slot = "1Hand", Gold = 600, Bonus = 4, Name = "Mace of Sharpness", Type = "Equipment", PictureUrl = "equipment/MaceofSharpness.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1 Hand,  Combat Power Bonus: +4", FlavorText = "For todays rapper on the go.", Slot = "1Hand", Gold = 600, Bonus = 4, Name = "Hammer of Kneecapping", Type = "Equipment", PictureUrl = "equipment/HammerofKneecapping.jpg" });
                context.Cards.Add(new EquipmentCard { Description = "Equipment Slot: 1 Hand,  Combat Power Bonus: +3", FlavorText = "Pay it a dollar and it may holler.", Slot = "1Hand", Gold = 400, Bonus = 3, Name = "Gentlemen's Club", Type = "Equipment", PictureUrl = "equipment/GentlemensClub.jpg" });




                context.SaveChanges();
                
            }
        }
    }
}
