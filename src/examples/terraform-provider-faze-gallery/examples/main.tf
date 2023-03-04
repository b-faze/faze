terraform {
  required_providers {
    fazegallery = {
      version = "0.1"
      source  = "faze.com/gallery/faze-gallery"
    }
  }
}

provider "fazegallery" {}

module "psl" {
  source = "./image"

  image_name = "Faze image 1"
}

output "psl" {
  value = module.psl.image
}
