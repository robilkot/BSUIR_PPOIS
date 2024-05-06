import random

import pygame

from entities.asteroid_large import AssteroidLarge
from entities.asteroid_medium import AssteroidMedium
from entities.asteroid_small import AssteroidSmall
from entities.base_entity import BaseEntity
from entities.spaceship import Spaceship


class Assteroids:
    def __init__(self):
        self._init_pygame()
        self.screen = pygame.display.set_mode((800, 600))
        self.clock = pygame.time.Clock()
        self.entities: list[BaseEntity] = []
        self.spaceship = Spaceship((400, 300), (32, 32))

        self.entities.append(self.spaceship)
        for _ in range(0, 15):
            self.entities.append(AssteroidSmall((random.randrange(0, 800),
                                                 random.randrange(0, 600)),
                                                (random.randrange(-30, 30) / 30,
                                                 random.randrange(-30, 30) / 30)))

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

        if is_key_pressed[pygame.K_RIGHT]:
            self.spaceship.rotate(clockwise=True)
        elif is_key_pressed[pygame.K_LEFT]:
            self.spaceship.rotate(clockwise=False)

        if is_key_pressed[pygame.K_UP]:
            self.spaceship.accelerate()

    def _process_logic(self):
        for entity in self.entities:
            entity.move(self.screen)

    def _render(self):
        self.screen.fill((20, 20, 50))

        for entity in self.entities:
            entity.draw(self.screen)

        pygame.display.flip()
        # todo: control speed here?
        self.clock.tick(60)
