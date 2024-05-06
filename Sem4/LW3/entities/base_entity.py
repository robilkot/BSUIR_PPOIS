from __future__ import annotations
import pygame
from pygame import Surface, Rect
from pygame.math import Vector2

from engine.physics import wrap_position


class BaseEntity:
    def __init__(self,
                 position: tuple | Vector2,
                 sprite: Surface,
                 velocity: tuple | Vector2):
        self.position = Vector2(position)
        self.sprite = sprite
        self.radius = sprite.get_width() / 2
        self.velocity = Vector2(velocity)

    def draw(self, surface: Surface):
        origin = self.position - Vector2(self.radius)
        surface.blit(self.sprite, origin)

    def move(self, surface: Surface):
        # todo: time provider? or move to physics
        self.position = wrap_position(self.position + self.velocity, surface)

    def collides_with(self, other_obj: BaseEntity) -> bool:
        distance = self.position.distance_to(other_obj.position)
        return distance < self.radius + other_obj.radius
