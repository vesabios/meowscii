INCLUDE global.ink

-> intro

=== intro ===

{ SetScene("intro") }

You are standing alone in wide open field of grass. A verdant vista stretched for miles, punctuated by a lonely stone castle.

+ [Go north]

- The open plains offer no shelter from the strong winds. You approach a large, rusted iron gate. It extends upwards beyond any reasonable height. 

+ [Press forward]

The gate must weigh thousands of pounds. With all of your weight you lean into it. It finally gives way with a deep booming reverberation.

- You look up and see tall stone walls - many stories high with very few features. Drenched in sunlight, you can clearly see that the structure is in disrepair, and crumbling in places. There are no signs of life other than a bird in the distance, and the mottled grasses struggling to survive in this wind bleached plain.

- There does appear to be an entryway ahead. 

+ [Follow what remains of a path ]

-> entryway


=== entryway ===


{ SetScene("castle") }


- You are at an entryway before a large castle. There is an unassuming wooden door here.

+ [Open the door]

- You open the door and walk into a spiral staircase that leads upwards. It takes you awhile but you eventually arrive, out of breath. 

-> hallway

=== hallway === 

{ SetScene("hallway") }

You stand at the far end of a long and wide hallway of polished stone. Elegant carpets and tapestries decorate the walls. Warm sunlight pours in through openings you cannot see, filling the hall with soft light. There are two openings to either side, far apart, and another door at the further end of the hall.

+ Library -> library

+ Study -> study

+ Mirror -> mirror

+ Fountain -> fountain

+ Door -> hallway_door

+ Down 
    You walk down the dizzying spiral staircase. -> entryway

-> DONE


=== library ===

{ SetScene("library") }


You are in the library. There are shelves on three of the four walls, replete with sliding ladders. The far wall is comprised mainly of large windows, overlooking a grassy vista. There is an elaborately decorated map of the world on a sphere.

+ Back to the hallway -> hallway

-> DONE

=== study ===

{ SetScene("study") }

You are in the study. The far wall is comprised of large windows, overlooking a grassy vista. There is a large desk.

* [Look at the desk.]

    It's made of wood and carved with ornate curls. The top of the desk is empty, save for a writing pad and a fountain pen.

+ [Back to the hallway] -> hallway

-> DONE

=== mirror ===

{ SetScene("mirror") }

You are in the mirror room. A standing, full length mirror occupies an otherwise barren room. You stand in front of it and see yourself.

+ [Back to the hallway] -> hallway

-> DONE

=== fountain ===

{ SetScene("fountain") }

You are in a room with a fountain. The mechanism which operates it cannot be seen. It's got a statue of marble in the middle, with a woman holding a pitcher of water. Water continuously pours into a pool below.

+ [Back to the hallway] -> hallway

-> DONE

=== hallway_door ===

{ SetScene("hallway_door") }

You are looking at a wooden door.

+ [Back to the hallway] -> hallway
+ [Through the door] -> passage

-> DONE




=== passage ===

{ SetScene("passage") }

You are in a dark passage. The rough stones offer you something to put your hands on, but you can't see very far at all.

+ [Continue]

- As you make your way down the tunnel, you stumble upon a torch.

+ [Pick up the torch]

- You pick up the torch. You happen to have a piece of flint and steel, which you promptly use to light the torch.

You continue on down the passage again.

+ [Press on...]

- Suddenly a goblin jumps out of nowhere at you! 

{ SetScene("goblin") }

+ [Fight the goblin]

    - You take the initiative and swing the torch at the goblin, landing a good blow to the side of his slimy head. After the dull thud of his skull hitting the wall, it is quiet again.
    
    * [Catch your breath] -> guardpost
    
+ [Run]

    - Scared to death, you turn around and sprint back down the hall from whence you came. ->passage



=== guardpost ===

{ SetScene("guardpost") }


After catching your breath, you take stock of the situation and realize you've sumbled upon some kind of guardpost. There's a nook in the wall with a stool for sitting. 

* [Rifle through the goblin's stuff]

- You look through the goblin's stuff and find a surprisingly decent sandwich. He was armed with a short sword, which, fortunately for you, was sitting with his lunch.

* [Take the stuff]

- Once you're satisfied that you've done what you can do here, you continue further on once again.


-> DONE

