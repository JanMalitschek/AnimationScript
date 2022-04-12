targets
main

keys
#comment
# set original transform
main 0 0 0 vec loc 0 0 0 eul rot 1 1 1 vec scl 0s key
# move to different location and scale
main 1 0 0 vec loc 90 0 0 eul rot 1 1 1 vec scl 1% key
# move back to original transform
main 0 0 0 vec loc 0 0 0 eul rot 1 1 1 vec scl 2s key