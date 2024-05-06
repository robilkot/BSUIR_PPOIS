import random

from engine.repository import load_sprite
from entities.asteroid import Assteroid


class AssteroidSmall(Assteroid):
    SmallAssteroidsNames = ["assteroid_small1", "assteroid_small2", "assteroid_small3"]

    def __init__(self, position, velocity, destroy_assteroid_callback, create_assteroid_callback):
        self.mass = 200
        sprite_name = random.choice(self.SmallAssteroidsNames)
        self.original_sprite = load_sprite(sprite_name, True, (16, 16))

        super().__init__(position, self.original_sprite, velocity, destroy_assteroid_callback, create_assteroid_callback)

    def destroy(self):
        self.destroy_assteroid_callback(self)
