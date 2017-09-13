== function d2 ==
{shuffle:
 - ~ return 1
 - ~ return 2
}

== function d3 ==
{shuffle:
 - ~ return 1
 - ~ return 2
 - ~ return 3
}

== function d4 ==
{shuffle:
 - ~ return 1
 - ~ return 2
 - ~ return 3
 - ~ return 4
}

== function d6 ==
{shuffle:
 - ~ return 1
 - ~ return 2
 - ~ return 3
 - ~ return 4
 - ~ return 5
 - ~ return 6
}

== function d8 ==
{shuffle:
 - ~ return 1
 - ~ return 2
 - ~ return 3
 - ~ return 4
 - ~ return 5
 - ~ return 6
 - ~ return 7
 - ~ return 8
}

== function d10 ==
{shuffle:
 - ~ return 1
 - ~ return 2
 - ~ return 3
 - ~ return 4
 - ~ return 5
 - ~ return 6
 - ~ return 7
 - ~ return 8
 - ~ return 9
 - ~ return 10
}

== function d12 ==
{shuffle:
 - ~ return 1
 - ~ return 2
 - ~ return 3
 - ~ return 4
 - ~ return 5
 - ~ return 6
 - ~ return 7
 - ~ return 8
 - ~ return 9
 - ~ return 10
 - ~ return 11
 - ~ return 12

}

== function d20 ==
{shuffle:
 - ~ return 1
 - ~ return 2
 - ~ return 3
 - ~ return 4
 - ~ return 5
 - ~ return 6
 - ~ return 7
 - ~ return 8
 - ~ return 9
 - ~ return 10
 - ~ return 11
 - ~ return 12
 - ~ return 13
 - ~ return 14
 - ~ return 15
 - ~ return 16
 - ~ return 17
 - ~ return 18
 - ~ return 19
}

=== function print_num(x) ===
{ 
    - x >= 1000:
        {print_num(x / 1000)} thousand { x mod 1000 > 0:{print_num(x mod 1000)}}
    - x >= 100:
        {print_num(x / 100)} hundred { x mod 100 > 0:and {print_num(x mod 100)}}
    - x == 0:
        zero
    - else:
        { x >= 20:
            { x / 10:
                - 2: twenty
                - 3: thirty
                - 4: forty
                - 5: fifty
                - 6: sixty
                - 7: seventy
                - 8: eighty
                - 9: ninety
            }
            { x mod 10 > 0:<>-<>}
        }
        { x < 10 || x > 20:
            { x mod 10:
                - 1: one
                - 2: two
                - 3: three
                - 4: four        
                - 5: five
                - 6: six
                - 7: seven
                - 8: eight
                - 9: nine
            }
        - else:     
            { x:
                - 10: ten
                - 11: eleven       
                - 12: twelve
                - 13: thirteen
                - 14: fourteen
                - 15: fifteen
                - 16: sixteen      
                - 17: seventeen
                - 18: eighteen
                - 19: nineteen
            }
        }
}