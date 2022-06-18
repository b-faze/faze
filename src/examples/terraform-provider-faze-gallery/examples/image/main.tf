terraform {
  required_providers {
    fazegallery = {
      version = "0.1"
      source  = "faze.com/gallery/faze-gallery"
    }
  }
}

variable "image_name" {
  type    = string
  default = "Faze default image"
}

data "fazegallery_images" "all" {}

# Returns all coffees
output "all_images" {
  value = data.fazegallery_images.all.images
}

# Only returns packer spiced latte
output "image" {
  value = {
    for image in data.fazegallery_images.all.images :
    image.id => image
    if image.name == var.image_name
  }
}
