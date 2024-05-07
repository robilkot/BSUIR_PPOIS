import random

import pygame
from pygame import Vector2, Surface

from engine.repository import load_sprite, load_sound
from entities.base_entity import BaseEntity
from entities.bullet import Bullet
from entities.spaceship import Spaceship


class Ufo(BaseEntity):
    # todo: config
    BulletSpeed = 3
    Speed = 1
    CoolDown = 1000
    DirectionChangeInterval = 1500

    def __init__(self, position, size, create_bullet_callback, player_spaceship: Spaceship):
        self.create_bullet_callback = create_bullet_callback
        self.direction = Vector2((0, -1))
        self.player_spaceship = player_spaceship
        self._laser_sound = load_sound("laser")
        self._last_shoot = pygame.time.get_ticks()
        self._last_direction_change = pygame.time.get_ticks()

        super().__init__(position, load_sprite("ufo", True, size), Vector2(0))

    def move(self, surface: Surface):
        self.shoot()

        now = pygame.time.get_ticks()
        if now - self._last_direction_change >= self.DirectionChangeInterval:
            self._last_direction_change = now

            rotated_velocity = Vector2(self.Speed)
            rotated_velocity.rotate_ip(45 * random.randint(-1, 1))
            self.velocity = rotated_velocity

        self.position += self.velocity

    def shoot(self):
        if self.player_spaceship is None:
            return

        now = pygame.time.get_ticks()
        if now - self._last_shoot >= self.CoolDown:
            self._last_shoot = now
            bullet_velocity = (self.player_spaceship.position - self.position).normalize() * self.BulletSpeed
            bullet = Bullet(self.position, bullet_velocity, can_hurt_player=True)
            self.create_bullet_callback(bullet)
            self._laser_sound.play()
