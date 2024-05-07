import pygame
from pygame import Vector2

from model.repository import load_sprite, load_sound
from model.entities.base_entity import BaseEntity
from view.graphics import rotate_image
from model.entities.bullet import Bullet


class Spaceship(BaseEntity):
    # todo: config
    BulletSpeed = 3
    RotationSpeed = 5
    Acceleration = 0.2
    CoolDown = 300

    def __init__(self, position, size, create_bullet_callback):
        self.create_bullet_callback = create_bullet_callback
        self.direction = Vector2((0, -1))
        self._original_sprite = load_sprite("spaceship", True, size)
        self._laser_sound = load_sound("laser")
        self._last_shoot = pygame.time.get_ticks()

        super().__init__(position, self._original_sprite, Vector2(0))

    def accelerate(self):
        self.velocity += self.direction * self.Acceleration

    def rotate(self, clockwise=True):
        sign = 1 if clockwise else -1
        angle = self.RotationSpeed * sign
        self.direction.rotate_ip(angle)

    def draw(self, surface):
        angle = self.direction.angle_to((0, -1))
        self.sprite, rect = rotate_image(self._original_sprite,
                                         self.position,
                                         (self._original_sprite.get_width() / 2,
                                          self._original_sprite.get_height() / 2),
                                         angle)

        surface.blit(self.sprite, rect)

    def shoot(self):
        now = pygame.time.get_ticks()
        if now - self._last_shoot >= self.CoolDown:
            self._last_shoot = now
            bullet_velocity = self.direction * self.BulletSpeed + self.velocity
            bullet = Bullet(self.position, bullet_velocity)
            self.create_bullet_callback(bullet)
            self._laser_sound.play()
