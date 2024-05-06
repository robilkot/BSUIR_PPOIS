import random

from pygame import Vector2, Surface

from engine.graphics import rotate_image
from entities.base_entity import BaseEntity


class Assteroid(BaseEntity):
    def __init__(self, position, sprite: Surface, velocity):

        self.sprite, self.rect = rotate_image(sprite,
                                             position,
                                             (sprite.get_width() / 2,
                                              sprite.get_height() / 2),
                                              random.randrange(0, 360))

        super().__init__(position, self.sprite, velocity)
