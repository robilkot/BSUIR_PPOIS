import random

from engine.repository import load_sprite
from entities.asteroid import Assteroid
from entities.asteroid_medium import AssteroidMedium


class AssteroidLarge(Assteroid):
    LargeAssteroidsNames = ["assteroid_large1", "assteroid_large2", "assteroid_large3"]

    def __init__(self, position, velocity, destroy_assteroid_callback, create_assteroid_callback):
        self.mass = 600
        self.score = 100
        sprite_name = random.choice(self.LargeAssteroidsNames)
        self.original_sprite = load_sprite(sprite_name, True, (64, 64))

        super().__init__(position, self.original_sprite, velocity, destroy_assteroid_callback, create_assteroid_callback)

    def destroy(self):
        children_pos_vel = [(self.velocity.normalize() * 15, self.velocity.copy() * 1.2) for _ in range(2)]
        children_pos_vel[0][0].rotate_ip(-90)
        children_pos_vel[0][1].rotate_ip(-25)
        children_pos_vel[1][0].rotate_ip(90)
        children_pos_vel[1][1].rotate_ip(25)
        children_pos_vel = [(pos[0] + self.position, pos[1]) for pos in children_pos_vel]

        children = [AssteroidMedium(pos_vel[0], pos_vel[1],
                                    self.destroy_assteroid_callback,
                                    self.create_assteroid_callback) for pos_vel in children_pos_vel]

        for child in children:
            self.create_assteroid_callback(child)

        self.destroy_assteroid_callback(self)
