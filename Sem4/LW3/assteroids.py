from __future__ import annotations

import pygame

from engine.helpers import get_random_position, get_random_velocity
from engine.physics import process_collisions
from entities.asteroid import Assteroid
from entities.asteroid_large import AssteroidLarge
from entities.asteroid_medium import AssteroidMedium
from entities.base_entity import BaseEntity
from entities.bullet import Bullet
from entities.spaceship import Spaceship


class Assteroids:
    MinAssteroidsDistance = 250

    def __init__(self):
        self._init_pygame()
        self.screen = pygame.display.set_mode((800, 600))
        self.clock = pygame.time.Clock()

        self.bullets: list[Bullet] = []
        self.spaceship = Spaceship((400, 300), (32, 32), self.bullets.append)
        self.assteroids: list[Assteroid] = []
        self.lives = 3

        self._start_new_game()

    def _get_game_objects(self) -> list[BaseEntity]:
        game_objects = [*self.assteroids, *self.bullets]

        if self.spaceship:
            game_objects.append(self.spaceship)

        return game_objects

    def _start_new_game(self):
        self.lives = 3
        self._play_new_level()

    def _play_new_level(self):
        assteroid_destroy_self_callback = self.assteroids.remove
        assteroid_destroy_children_callback = self.assteroids.append

        # todo config
        for _ in range(6):
            while True:
                position = get_random_position(self.screen)
                if position.distance_to(self.spaceship.position) > self.MinAssteroidsDistance:
                    break

            self.assteroids.append(AssteroidLarge(position, get_random_velocity(3, 30) / 15,
                                   assteroid_destroy_self_callback,
                                   assteroid_destroy_children_callback))

    def _die(self):
        self.lives -= 1

        if self.lives == 0:
            pass
            # todo end game

    def main_loop(self):
        while True:
            self._handle_events()
            self._process_logic()
            self._render()

    def _init_pygame(self):
        pygame.init()
        pygame.display.set_caption("Assteroids")

    def _handle_events(self):
        for event in pygame.event.get():
            if event.type == pygame.QUIT or event.type == pygame.KEYDOWN and event.key == pygame.K_ESCAPE:
                quit()
            elif self.spaceship and event.type == pygame.KEYDOWN and event.key == pygame.K_SPACE:
                self.spaceship.shoot()

        is_key_pressed = pygame.key.get_pressed()

        if self.spaceship:
            if is_key_pressed[pygame.K_RIGHT]:
                self.spaceship.rotate(clockwise=True)
            elif is_key_pressed[pygame.K_LEFT]:
                self.spaceship.rotate(clockwise=False)

            if is_key_pressed[pygame.K_UP]:
                self.spaceship.accelerate()

    def _process_logic(self):
        if len(self.assteroids) == 0:
            self._play_new_level()

        for entity in self._get_game_objects():
            entity.move(self.screen)

        for bullet in self.bullets:
            if not self.screen.get_rect().collidepoint(bullet.position):
                self.bullets.remove(bullet)

        if self.spaceship:
            for asteroid in self.assteroids:
                if asteroid.collides_with(self.spaceship):
                    # todo end game
                    self.spaceship = None
                    break

        for bullet in self.bullets:
            for asteroid in self.assteroids:
                if asteroid.collides_with(bullet):
                    self.bullets.remove(bullet)
                    asteroid.destroy()
                    break

        process_collisions(self.assteroids)

    def _render(self):
        self.screen.fill((20, 20, 50))

        for entity in self._get_game_objects():
            entity.draw(self.screen)

        pygame.display.flip()
        # todo: control speed here?
        self.clock.tick(60)

    def _render_interface(self, surface):
        pass
