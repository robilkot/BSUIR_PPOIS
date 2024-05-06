import random

from engine.repository import load_sprite
from entities.asteroid import Assteroid


class AssteroidSmall(Assteroid):
    SmallAssteroidsNames = ["assteroid_small1", "assteroid_small2", "assteroid_small3"]

    def __init__(self, position, velocity):
        sprite_name = random.choice(self.SmallAssteroidsNames)
        self.original_sprite = load_sprite(sprite_name, True, (16, 16))

        super().__init__(position, self.original_sprite, velocity)
