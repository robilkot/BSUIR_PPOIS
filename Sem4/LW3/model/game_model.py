import threading

import pygame

from controller.event_listener import EventListener
from controller.event_manager import *
from helpers.helpers import get_random_position, get_random_velocity
from model.entities.asteroid import Assteroid
from model.entities.asteroid_large import AssteroidLarge
from model.entities.base_entity import BaseEntity
from model.entities.bullet import Bullet
from model.entities.spaceship import Spaceship
from model.entities.ufo import Ufo
from model.game_state_machine import STATE_MENU, StateMachine, STATE_PLAY, STATE_GAME_OVER
from model.physics import process_collisions


class GameModel(EventListener):
    SafeSpawnDistance = 200

    def __init__(self, event_manager):
        self.event_manager = event_manager
        event_manager.register_listener(self)
        self.running = False
        self.state_stack = StateMachine()

        self.screen = pygame.display.get_surface()

        self.ufos: list[Ufo] = []
        self.bullets: list[Bullet] = []
        self.assteroids: list[Assteroid] = []
        self.lives = 3
        self.score = 0
        self.player_name = None
        self.game_is_best = False

        self.starting_new_level = False

    def notify(self, event):
        if isinstance(event, GameOverEvent):
            self.state_stack.pop()
            self.state_stack.push(STATE_GAME_OVER)

        elif isinstance(event, QuitEvent):
            pygame.quit()
            exit()

        elif isinstance(event, StateChangeEvent):
            # pop request
            if not event.state:
                self.running = False
                self._clear_game()

                # false if no more states are left
                if not self.state_stack.pop():
                    self.event_manager.post(QuitEvent())
            else:
                if event.state == STATE_PLAY:
                    self._start_new_game()

                self.state_stack.push(event.state)

    def run(self):
        self._start_new_game()

        self.event_manager.post(InitializeEvent())

        self.state_stack.push(STATE_MENU)
        while True:
            if self.running:
                self._process_logic()

            tick_event = TickEvent()
            self.event_manager.post(tick_event)

    # todo save to table
    def save_record(self):
        pass
        # self.player_name

    def get_game_objects(self) -> list[BaseEntity]:
        game_objects = [*self.assteroids, *self.bullets, *self.ufos]

        if self.spaceship:
            game_objects.append(self.spaceship)

        return game_objects

    def _clear_game(self):
        self.game_is_best = False
        self.assteroids = []
        self.ufos = []
        self.spaceship = None

    def _die(self):
        self.lives -= 1
        self.spaceship = None
        for ufo in self.ufos:
            ufo.player_spaceship = None

        if self.lives == 0:
            # todo is game best
            self.game_is_best = True
            self.event_manager.post(GameOverEvent())
            return

        t = threading.Timer(1.5, self._create_spaceship, args=[])
        t.start()

    def _process_logic(self):
        if not self.starting_new_level and len(self.assteroids) == 0:
            self.starting_new_level = True
            t = threading.Timer(1.5, self._play_new_level, args=[])
            t.start()

        for entity in self.get_game_objects():
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

    def _start_new_game(self):
        self._clear_game()
        self._create_spaceship()
        self.lives = 3
        self.score = 0
        self._play_new_level()
        self.running = True

    def _play_new_level(self):
        def assteroid_destroy_self_callback(assteroid: Assteroid) -> None:
            self.assteroids.remove(assteroid)
            self.score += assteroid.score

        assteroid_destroy_children_callback = self.assteroids.append

        # todo config
        for _ in range(6):
            while True:
                position = get_random_position(self.screen)
                if position.distance_to(self.spaceship.position) > self.SafeSpawnDistance:
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
