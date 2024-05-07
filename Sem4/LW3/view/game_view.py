import pygame
from controller.event_listener import EventListener
from controller.event_manager import *
from model.game_state_machine import STATE_PLAY, STATE_HELP, STATE_MENU, STATE_GAME_OVER, STATE_RECORDS
from model.repository import load_sprite


class AssteroidsView(EventListener):
    def __init__(self, event_manager, game_model):
        self.event_manager = event_manager
        event_manager.register_listener(self)
        self.model = game_model
        self.is_initialized = False
        self.screen = None
        self.clock = None
        self.small_font = None
        self.large_font = None
        self.selected_menu_option = 0
        self.current_menu_options = []

    def notify(self, event):
        if isinstance(event, InitializeEvent):
            self.initialize()
        elif isinstance(event, QuitEvent):
            self.is_initialized = False
        elif isinstance(event, MenuItemSelectionChangedEvent):
            self.selected_menu_option += 1 if event.increase else -1
            self.selected_menu_option %= len(self.current_menu_options)
        elif isinstance(event, MenuItemSelectedEvent):
            self.event_manager.post(StateChangeEvent(self.current_menu_options[self.selected_menu_option][1]))
        elif isinstance(event, TickEvent):
            if not self.is_initialized:
                return
            current_state = self.model.state_stack.peek()
            if current_state == STATE_MENU:
                self.render_menu()
            if current_state == STATE_PLAY:
                self.render_play()
            if current_state == STATE_HELP:
                self.renderhelp()
            if current_state == STATE_GAME_OVER:
                self.rendergameover()

            self.clock.tick(60)

    def render_menu(self):
        self.current_menu_options = [
            ("New game", STATE_PLAY),
            ("About game", STATE_HELP),
            ("Records table", STATE_RECORDS),
            ("Exit", None),
        ]
        self.screen.fill((0, 0, 0))

        somewords = self.large_font.render(
            'Welcome to Assteroids',
            True, (255, 255, 255))
        self.screen.blit(somewords, (100, 100))

        for index, option in enumerate(self.current_menu_options):
            selected = index == self.selected_menu_option
            color = (0, 100, 200) if selected else (0, 40, 80)
            option_text = self.small_font.render(option[0], True, color)

            pos_y = 200 + index * 40
            self.screen.blit(option_text, (100, pos_y))

            if selected:
                pygame.draw.rect(self.screen, color, pygame.Rect(100 - 10, pos_y, 2, 20))

        pygame.display.flip()

    def render_play(self):
        self.screen.fill((20, 20, 50))

        for entity in self.model.get_game_objects():
            entity.draw(self.screen)

        self._render_game_interface(self.screen)

        pygame.display.flip()

    def renderhelp(self):
        self.screen.fill((0, 0, 0))
        somewords = self.small_font.render(
            'Help is here. space, escape or return.',
            True, (0, 255, 0))
        self.screen.blit(somewords, (0, 0))
        pygame.display.flip()

    def rendergameover(self):
        self.screen.fill((0, 0, 0))
        somewords = self.small_font.render(
            'Help is here. space, escape or return.',
            True, (0, 255, 0))
        self.screen.blit(somewords, (0, 0))
        pygame.display.flip()

    def initialize(self):
        pygame.init()
        pygame.font.init()
        self.screen = pygame.display.get_surface()
        self.clock = pygame.time.Clock()
        self.small_font = pygame.font.Font(None, 30)
        self.large_font = pygame.font.Font(None, 60)
        self.is_initialized = True

    def _render_game_interface(self, surface):
        spaceship_sprite = load_sprite("spaceship", size=(45, 45))
        lives_bar_x = surface.get_rect().width / 5
        lives_bar_y = 30

        for i in range(0, self.model.lives):
            surface.blit(spaceship_sprite, (lives_bar_x + i * 50, lives_bar_y, 32, 32))

        # print_text(surface, str(self.score), self.font, (surface.get_width() - lives_bar_x, lives_bar_y, 50, 50))
