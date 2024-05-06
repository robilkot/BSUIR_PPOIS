import random

from engine.repository import load_sprite
from entities.asteroid import Assteroid


class AssteroidLarge(Assteroid):
    LargeAssteroidsNames = ["assteroid_large1", "assteroid_large2", "assteroid_large3"]

    def __init__(self, position, velocity):
        sprite_name = random.choice(self.LargeAssteroidsNames)
        self.original_sprite = load_sprite(sprite_name, True, (64, 64))

        super().__init__(position, self.original_sprite, velocity)
