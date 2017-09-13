INCLUDE Globals.ink
INCLUDE Oracle.ink



-> inn_room
== inn_room

You're in Luska, [City of Sails]. You've been in town only a short time. You were working aboard a ship which arrived here and have been spending the days looking for new adventures.

You wake up at the Cutlass, a seedy inn on Half-Moon Street near the city's piers.

->DONE

-*[...]

As you make your way down for breakfast, trying not to disturb the drunks on the stairs, you notice the barkeep, Arumn Gardpeck, tacking something up to the door. The notice reads: <>    "Wanted skilled man-at-arns to accompany a merchant into grand adventure in the city Targos in exotic Icewind Dale. Pay will be commensurate with skill. Please see Master Peddywinkle, merchant to the Realms, currently staying in the Royal Arms outside the Winter Palace. Employment in Targos after the trip is also offered., Hurry! The adventure awaits!"

-(top)

* [Ask Arumn about Peddywinkle.]

    "Arum is a friendly merchant from Waterdeep who is often seen in town. He usually trades in small scrimshaw pieces made from the prized knucklehead trout found only in the lakes of Icewind Dale. He is also said to pay pretty well. ->top

* [Head to the Royal Arms.] ->royal_arms


== royal_arms

The Royal Arms is a modern beauty of an inn - much nicer (and much more expensive) than the Cutlass. In fact, this three-story inn is exactly where you would like to stay, if you could afford it! Opening the huge double doors, you see before you the life you imagine is lived by kings and princes; your eyes feast upon rich tapestries, golden statues, and a sweeping grand staircase and your nose delights to the aromas of fine food and wine. There are only a few patrons sitting in the cozy dining area, and the barkeep glares at youfrom over his spectacles. It is fairly obvious to him that you won't be paying customers.

Bartholomew Tidfigit is the owner. Tall and lean, he looks more like a scholar than a barkeep.

-(top)

* [Ask about Peddywinkle]
    He points you to a regal-looking fellow in courtly dress and a handlebar mustache, who is sipping a cup of tea and eating a muffin. 

-* [Approach man in corner]

The man in the comer looks you over as you approach, as if to size you up. He motions for you to sit. 

"Good day, my friend!"

-*[...]

"I assume that you are here in response to my post. Good. I truly want to get started right away. As you may remember, I stated that pay would be commensurate with skill. Exactly how many undertakings of this sort have you experienced?"

-
*"First time."
*"A few."
*"More than you!"
-
I see. Well, based on that I think I can afford to pay you  50 gold pieces once we reach Targos, plus supply your food and water for the journey. And if you are willing to stay on for awhile, I could pay you a share of the profits from a few other ventures I have up thereand those could be quite substantial. I'm leaving in three hours. The journey to Icewind Dale takes approximately two tendays. Does that sound acceptable?"

*"My fee is 200."
    "And yet you will only receive 60."
    **"Did I stutter?"
        "How does 80 sound?"
        ***"Laughable."
            "100 is my final offer. Take it or leave it."
            ****"Very well..."
    **"That will do."
*"Sounds great."

-

*"What will I be guarding?"
    "A shipment of exotic foodstuffs and spices from across Toril that are seldom seen that far north.
    **"OK"
    **"Why?"
        "I wish to purchase some scrimshaw while in Icewind Dale. That can be quite expensive."
        ***"Keep talking."
        "What else is there to say? Stores of cinnamon sticks, bay leaves, ground cumin, other spices, a selection of cheeses -- including death cheese (made from catoblepas milk) -- and honeycombs fresh from beehives far to the south.
        
-*"Ok, Let's go..."

As you circle around the back of the Royal Arms, you see a middle-aged, black-haired man in leather armor harnessing a pair of horses to a large covered wagon. 

-*[...]

He eyes you suspiciously at first, then nods in greeting, and goes back to his work. Behind him you see a tall, brown-haired boy of about 16 years carrying a box of tools out of the stables, heading towards a second wagon. Seeing you, his face lights up in a wide grin, and he drops his box and approaches.

-*[...]

    "Good morning, sirs (and ladies), you must be the grand adventurers that Master Peddywinkle told us would be guarding the caravan! "
    
    -*[...]

    "I'm Dell. Dell Tannerson. That's my Uncle Rafferty over there. He's teaching me everything he knows about being a teamster, you know, and that's quite a lot I must tell you, although I fear it isn't nearly as exciting as what you do." 
    
    -*[...]

    Shaking your hand vigorously and not letting you get a word in, he says, "Oh, the stories you have to tell must be full of wonder! Please, you MUST share with me the tales of your adventures as soon as we are underway, or even show me some magic! I do love seeing magic, especially when it makes things disappear. 
    
    -*[...]

    "I once saw someone make a horse disappear and then reappear again. That was amazing!"
    
    -*[...]

    "I wonder how he did it? Do you know? Oh well, I haven't the time to talk right now. I have to get back to work before Uncle Raff gets angry. It was very nice meeting you."
    
As you stand there in wonder as to how anyone could say that much in one breath, Dell picks up the box and heads toward the second wagon.

-*[...]

After a few minutes, the horses are attached to the wagons and the supplies are loaded. You then see Master Peddywinkle come around the corner toward you. 

-*[...]

"Ah my friends, I see you are punctual. I like that. Everything seems to be in order here, so take your positions and let us be on our way."

-*[...]

With that, he climbs up into the back of the first wagon and drops the flap.


->the_journey


=== the_journey ===

It's spring (Tarsakh) of 1369 DR, when the adventure begins, but winter's teeth can still bite this far north, especially in the mountains.

The temperature hovers around 40s (degrees Fahrenheit) and nighttime lows in the 20s, with a blustery wind during the day that dies down once the sun sets. Travellers who take reasonable precautions in dressing for the weather should have few problems unless they get wet or somehow lose their gear.

In the mountains, high winds also make it feel colder than it is.

-*[...]

You start out on the adventure south of the Spine of the World Mountains. It's fairly easy traveling, over well-cut roads, through tamed forests, and along cultivated fields.

~ temp DAY = 0

->journey_loop
= journey_loop
~ DAY++

{ 
    - DAY < 12:

        {~The mountains loom larger and larger as you continue into day {DAY}.|||}

        {
            - d10() == 1:
                -> day_encounter(-> journey_loop)
            - else:
                { shuffle:
                    - -> stuck_in_mud
                    - -> broken_wheel
                    - -> journey_loop
                    - -> snow_drift
                }
        }
        
    - DAY == 12:
        The trail before you climbs slowly but steadily, and great slabs of stone begin to replace the fields and trees at the sides of the trail - perfect hiding places for potential ambushes you note - while other great outcroppings hang precariously above you. Still, the trail remains fairly smooth and straight, and the wagons roll along uphill.

    - DAY == 13:
        ->encounter_in_the_pass(-> journey_loop)
        
    - DAY == 14:
        Day{DAY}

    - DAY == 15:
        As suddenly as the mountains began, three mornings past, they now end, and all the northland opens wide before you, as if you have emerged from a dim tunnel into a bright sun. Waist-high brown-and-green grass covers the tundra before you, the sudden and almost explosive spring growth that marks this forlorn land. The vista before you seems almost completely flat, not a mountain or a hillock to be seen, offering an unbroken view to the horizon. But as much as your view has widened with your leaving the mountains, your hearing seems to diminish, for a mournful, monotonous, and continuous wind thrums in your ears, a constant background noise that forces you to raise your voice even to converse with your companions.      
  
        
    - DAY >15 && DAY<20:

        The mountains loom larger and larger as you continue into day {DAY}

        {
            - d10() == 1:
                -> day_encounter(-> journey_loop)
            - else:
                { shuffle:
                    - -> stuck_in_mud
                    - -> broken_wheel
                    - -> journey_loop
                    - -> snow_drift
                }
        }
        
    - DAY == 20:
        The wagons roll across a small bridge, a small but swiftmoving river rushing by beneath. "The Shaengarne tributary!" Master Peddywinkle cries, obviously overjoyed. "Almost there! We'll see the peak of Kelvin's Cairn, the lone mountain in these parts, before the dusk, and tomorrow we'll see the walls of Bryn Shander, or maybe Targos itself!"
        
        +[...]

        By the end of the day, you see the snowy summit of Kelvin's Cairn, and on the 21st day, they'll pass to the west of the walled town of Bryn Shander that sits atop a hill. There are no random encounters from this point on, for they are now within the region of Ten-Towns, as close to civilization as one can be north of the Spine of the World Mountains.
        
        ->end_journey
        
    - else:
    
        ->end_journey

 
}
-> journey_loop




= broken_wheel
    The wagon wheel breaks! 
    +[...] ->journey_loop


= stuck_in_mud
    The wagon gets stuck in the spring mud. 
    +[...] ->journey_loop

= goblin_attack
    You are attacked by goblins! 
    +[...] ->journey_loop


= snow_drift
    You are stopped by an impassable snowdrift. You spend a few hours clearing a way forward.
    +[...] ->journey_loop

->DONE

=== end_journey

The journey has ended.

->END


=== goblin_encounter (-> go_back_to) 
    You encounter a bunch of goblins.
    +[Kill goblins]
    You kill the goblins
    ->go_back_to
    
    
=== encounter_in_the_pass(-> go_back_to)

As you start your climb to the pass, you notice that the slopes are sparsely covered with trees, mostly pines. Mountain laurel flourishes here, forming a never-ending carpet across the slopes. Where the flora fails to find a foothold, the ground is strewn with huge rock formations. The highest slopes are covered with snow and topped by white clouds.

Peddywinkle calls for the camp to be set on the eastern side of the pass near a shallow pool amidst some trees.

* Explore the pool
    You notice what looks to be bone partially buried about two feet from the pool's edge.
    ** Retrieve bone.
        YOu find that it is actually a scroll case containing one priest scroll with nine spells: 1st level cure light wounds X 2, detect magic, endure cold X 2, light; 2nd level obscurement, slow poison, and water breathing. The scroll case seems to have been buried recently, as it bears no marks of enduring much of the region's harsh weather. 
       
        
-

Dell finds the remains of a destroyed wagon just north of the camp. In a depression near the wagon are 18 bodies: three merchants, four guards, six drivers, and five goblins. All the bodies have been stripped clean, though it appears as if the battle occurred less than a day before. 

-* Examine carefully
    Careful examination of the area reveals a set of wagon tracks heading up the eastern slope, along with the footprints of a half dozen or so humanoids.
    
-* Follow the tracks...

As you ascend the slopes, you are amazed at the sheer volume of mountain laurel. Someone, even a giant, could easily hide amidst the leafy growth! The wagon tracks remain clear, as the passage of the obviously heavy cart has cut a swath through the tangle. 


-*[...]

After ascending about 120 feet, you see that the tracks lead to a clearing amidst the laurel.

-
{~|You hear the noise of a nearby goblin camp!}

* Veer to the north
* Follow the tracks
* Veer to the south

-
The goblin encampment is set up as such: three covered wagons in a semicircle with a small campfire at the center. Three goblins, dressed in hides, stand alongside wagon number one, arguing over some of the garments of their victims. Near the campfire is an ogre mumbling to itself while quaffing ale from a large keg. There is another, empty keg lying near him. 
* Examine wagon 2
    There's a goblin here busy pryling loose at the floorboards.
    
* Examine Wagon 3
    It's been destroyed and rummaging through the remains is a lone goblin.
    
-

About 30 feet southeast of the wagons is a pool of water about four feet deep, 20 feet in diameter, and surrounded by overgrowth and laurel. 

*[...]

Here the goblin leader, wearing chain mail with a fine-looking silver-inlaid short sword and an archer with a short bow, ten arrows, and a dagger refresh themselves from the pool.

-
* Attack now 
    You surprise your foes and catch the camp's members totally unawares. Also, if the goblin leader and the archer are not attacked at the same time as the rest of the camp, the archer tries to hide in the rocks on the northwest side of the pool and snipe at the PCs with his bow. The leader hides near the pool underneath the overhanging growth on the northern bank. Any player within 30 feet of the pool hears him splashing in the shallow water. The leader joins in the fighting if things are going well for his minions. If not, he tries to slip away.

* Watch and wait
    As the night approaches, the goblins appear to become more alert and active. It is obvious that goblins prefer the night. 
    
-
    
* Search caravan
    You find three small chests. Chest number one contains rabbit furs worth 20 gp. 
    
    Chest two contains scrimshaw worth 17 gp, 
    
    Chest number three contains two potions of healing and one silver ring worth 15 gp. 
    
    Hidden under the floorboards of wagon one is a pouch containing 5 gp, 10 sp, and a wizard's scroll with five spells: 1st charm person, detect magic, hypnotism, light, sleep. 
    
    These identity the slain remains a mystery, as the goblins have done a fine job of ransacking the wagons and destroying any records.

-
-> go_back_to




=== day_encounter(-> origin)
{shuffle:
    - -> wounded_yeti
    - -> scounting_party1
    - -> hungry_bear
    - -> wild_horses
    - -> insane_ogre
    - -> scouting_party2
    - -> destroyed_caravan
    - -> frost_lizards
}

= wounded_yeti
   You discover a trail of blood that crosses the path. When you follow the blood you come across the yeti trying to hide. The yeti will not attack, but if forced to defend itself, it does so. 
   
    +[Battle]
        -> battle("wounded_yeti", origin)
    +[Run] ->origin

= scounting_party1
    You are ambushed by a scouting party!

    The group consists of four orcs and a gnoll. The group attacks the party, believing they have found an easy target.
    +[Battle]
        -> battle("scounting_party1", origin)
    +[Run] ->origin
    
    
= hungry_bear

    Suddenly a hungry bear emerges from the wild.

    This bear is not particularly interested in the party, except that the aroma of the caravan's food is drawing it near. The bear's only concern is the food, but if attacked, it fights back.
    +[Battle]
        -> battle("hungry_bear", origin)
    +[Run] ->origin


= wild_horses
    The party sees a herd of the wild, shaggy horses that live this far north running on open ground.
    +[Battle]
        -> battle("wild_horses", origin)
    +[Run] ->origin


= insane_ogre
    You come across an insane ogre!
    
    This ogre attacks the party on sight, despite the odds, because a rabid wolf bit it and its mind is half-gone. 
    
    +[Battle]
        ->battle("insane_ogre", origin)
    +[Run] ->origin


= scouting_party2
    You are ambushed by a scouting party!
    
    This scouting party consists of four goblins and a bugbear. They ambush the party along the road.
    +[Battle]
        ->battle("scouting_party2", origin)
    +[Run] ->origin


= destroyed_caravan
    You stumble upon a destroyed caravan.

    A caravan was ambushed by hobgoblins, and the wagons were pulled off the road to be looted. There was a battle that at least three beings didn't survive (two goblins, one dwarven warrior; any dwarf PCs have a 5% chance of knowing this dwarf, whose name was Regbald Stonethrower).
    +[Run] ->origin

= frost_lizards
    You stumble upon a pair of frost lizards!

    These magical lizard creatures attack on sight; they concentrate on the horses.
    
    +[Battle]
        -> battle("frost_lizards", origin)
    +[Run] ->origin



->DONE

