import random

from engine.repository import load_sprite
from entities.asteroid import Assteroid
from entities.asteroid_small import AssteroidSmall


class AssteroidMedium(Assteroid):
    MediumAssteroidsNames = ["assteroid_medium1", "assteroid_medium2", "assteroid_medium3"]

    def __init__(self, position, velocity, destroy_assteroid_callback, create_assteroid_callback):
        self.mass = 400
        sprite_name = random.choice(self.MediumAssteroidsNames)
        self.original_sprite = load_sprite(sprite_name, True, (32, 32))

        super().__init__(position, self.original_sprite, velocity, destroy_assteroid_callback, create_assteroid_callback)

    def destroy(self):
        children_pos_vel = [(self.velocity.normalize() * 10, self.velocity.copy() * 1.2) for _ in range(2)]
        children_pos_vel[0][0].rotate_ip(-90)
        children_pos_vel[0][1].rotate_ip(-20)
        children_pos_vel[1][0].rotate_ip(90)
        children_pos_vel[1][1].rotate_ip(20)
        children_pos_vel = [(pos[0] + self.position, pos[1]) for pos in children_pos_vel]

        children = [AssteroidSmall(pos_vel[0], pos_vel[1],
                                    self.destroy_assteroid_callback,
                                    self.create_assteroid_callback) for pos_vel in children_pos_vel]

        for child in children:
            self.create_assteroid_callback(child)

        self.destroy_assteroid_callback(self)
