import pygame
from pygame import Vector2

from engine.repository import load_sprite
from entities.base_entity import BaseEntity
from engine.graphics import rotate_image


class Spaceship(BaseEntity):
    # todo: config
    RotationSpeed = 4
    Acceleration = 0.25

    def __init__(self, position, size):
        self.direction = Vector2((0, -1))
        self.original_sprite = load_sprite("spaceship", True, size)

        super().__init__(position, self.original_sprite, Vector2(0))

    def accelerate(self):
        self.velocity += self.direction * self.Acceleration

    def rotate(self, clockwise=True):
        sign = 1 if clockwise else -1
        angle = self.RotationSpeed * sign
        self.direction.rotate_ip(angle)

    def draw(self, surface):
        angle = self.direction.angle_to((0, -1))
        self.sprite, rect = rotate_image(self.original_sprite,
                                         self.position,
                                         (self.original_sprite.get_width() / 2,
                                          self.original_sprite.get_height() / 2),
                                         angle)

        surface.blit(self.sprite, rect)
