import pygame
from pygame import Surface
from pygame.image import load
from pygame.mixer import Sound


def load_sprite(name, with_alpha=True, size: tuple[int, int] | None = None) -> Surface:
    path = f"assets/sprites/{name}.png"
    loaded_sprite = load(path)
    loaded_sprite = pygame.transform.scale(loaded_sprite, size) if size is not None else loaded_sprite

    if with_alpha:
        return loaded_sprite.convert_alpha()
    else:
        return loaded_sprite.convert()


def load_sound(name):
    path = f"assets/sounds/{name}.mp3"
    return Sound(path)
