import pygame
from pygame import Surface
from pygame.image import load


def load_sprite(name, with_alpha=True, size: tuple[int, int] | None = None) -> Surface:
    path = f"assets/sprites/{name}.png"
    loaded_sprite = load(path)
    loaded_sprite = pygame.transform.scale(loaded_sprite, size) if size is not None else loaded_sprite

    if with_alpha:
        return loaded_sprite.convert_alpha()
    else:
        return loaded_sprite.convert()
