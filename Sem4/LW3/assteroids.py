from __future__ import annotations

import threading

import pygame

from engine.graphics import print_text
from engine.helpers import get_random_position, get_random_velocity
from engine.physics import process_collisions
from engine.repository import load_sprite
from entities.asteroid import Assteroid
from entities.asteroid_large import AssteroidLarge
from entities.asteroid_medium import AssteroidMedium
from entities.base_entity import BaseEntity
from entities.bullet import Bullet
from entities.spaceship import Spaceship
from entities.ufo import Ufo


class Assteroids:
    MinAssteroidsDistance = 200

    def __init__(self):
        self._init_pygame()
        self.screen = pygame.display.set_mode((800, 600))
        self.clock = pygame.time.Clock()
        self.font = pygame.font.Font(None, 64)

        self.ufos: list[Ufo] = []
        self.bullets: list[Bullet] = []
        self.assteroids: list[Assteroid] = []
        self.lives = 3
        self.score = 0

        self.starting_new_level = False

        self._create_spaceship()
        self._create_ufo()
        self._start_new_game()

    def _get_game_objects(self) -> list[BaseEntity]:
        game_objects = [*self.assteroids, *self.bullets, *self.ufos]

        if self.spaceship:
            game_objects.append(self.spaceship)

        return game_objects

    def _start_new_game(self):
        self.lives = 3
        self._play_new_level()

    def _play_new_level(self):
        def assteroid_destroy_self_callback(assteroid: Assteroid) -> None:
            self.assteroids.remove(assteroid)
            self.score += assteroid.score

        assteroid_destroy_children_callback = self.assteroids.append

        # todo config
        for _ in range(8):
            while True:
                position = get_random_position(self.screen)
                if position.distance_to(self.spaceship.position) > self.MinAssteroidsDistance:
                    break

            self.assteroids.append(AssteroidLarge(position, get_random_velocity(3, 25) / 15,
                                                  assteroid_destroy_self_callback,
                                                  assteroid_destroy_children_callback))

        self.starting_new_level = False

    def _create_ufo(self):
        self.ufos.append(Ufo(get_random_position(self.screen), (32, 32), self.bullets.append, self.spaceship))

    def _create_spaceship(self):
        self.spaceship = Spaceship((400, 300), (32, 32), self.bullets.append)

        for ufo in self.ufos:
            ufo.player_spaceship = self.spaceship

    def _die(self):
        self.lives -= 1
        self.spaceship = None
        for ufo in self.ufos:
            ufo.player_spaceship = None

        if self.lives == 0:
            # todo end game
            return

        t = threading.Timer(1.5, self._create_spaceship, args=[])
        t.start()

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

        is_key_pressed = pygame.key.get_pressed()

        if self.spaceship:
            if is_key_pressed[pygame.K_RIGHT]:
                self.spaceship.rotate(clockwise=True)
            elif is_key_pressed[pygame.K_LEFT]:
                self.spaceship.rotate(clockwise=False)

            if is_key_pressed[pygame.K_SPACE]:
                self.spaceship.shoot()

            if is_key_pressed[pygame.K_UP]:
                self.spaceship.accelerate()

    def _process_logic(self):
        if not self.starting_new_level and len(self.assteroids) == 0:
            self.starting_new_level = True
            t = threading.Timer(1.5, self._play_new_level, args=[])
            t.start()

        for entity in self._get_game_objects():
            entity.move(self.screen)

        for bullet in self.bullets:
            if not self.screen.get_rect().collidepoint(bullet.position):
                self.bullets.remove(bullet)

        for ufo in self.ufos:
            if not self.screen.get_rect().collidepoint(ufo.position):
                self.ufos.remove(ufo)

        if self.spaceship:
            for asteroid in self.assteroids:
                if asteroid.collides_with(self.spaceship):
                    self._die()
                    break

        for bullet in self.bullets:
            if bullet.can_hurt_player:
                if self.spaceship and self.spaceship.collides_with(bullet):
                    self._die()

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

        self._render_interface(self.screen)

        pygame.display.flip()
        # todo: control speed here?
        self.clock.tick(60)

    def _render_interface(self, surface):
        spaceship_sprite = load_sprite("spaceship", size=(45, 45))
        lives_bar_x = surface.get_rect().width / 5
        lives_bar_y = 30
        for i in range(0, self.lives):
            surface.blit(spaceship_sprite, (lives_bar_x + i * 50, lives_bar_y, 32, 32))

        print_text(surface, str(self.score), self.font, (surface.get_width() - lives_bar_x, lives_bar_y, 50, 50))
