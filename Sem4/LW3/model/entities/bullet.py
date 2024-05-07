from model.repository import load_sprite
from model.entities.base_entity import BaseEntity


class Bullet(BaseEntity):
    def __init__(self, position, velocity, can_hurt_player=False):
        self.can_hurt_player = can_hurt_player
        super().__init__(position, load_sprite("bullet", True, (8, 8)), velocity)

    def move(self, surface):
        self.position = self.position + self.velocity
