import random

from engine.repository import load_sprite
from entities.asteroid import Assteroid


class AssteroidMedium(Assteroid):
    MediumAssteroidsNames = ["assteroid_medium1", "assteroid_medium2", "assteroid_medium3"]

    def __init__(self, position, velocity):
        sprite_name = random.choice(self.MediumAssteroidsNames)
        self.original_sprite = load_sprite(sprite_name, True, (32, 32))

        super().__init__(position, self.original_sprite, velocity)
