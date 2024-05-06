from engine.repository import load_sprite
from entities.base_entity import BaseEntity


class Bullet(BaseEntity):
    def __init__(self, position, velocity):
        super().__init__(position, load_sprite("bullet", True, (8, 8)), velocity)

    def move(self, surface):
        self.position = self.position + self.velocity
