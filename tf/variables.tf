variable "resource_group_name" {
  description = "Nome do Resource Group"
  type        = string
  default     = "rg-fastfood-postech"

  validation {
    condition     = length(var.resource_group_name) > 0
    error_message = "O nome do Resource Group não pode estar vazio."
  }
}


variable "location" {
  description = "Localização dos recursos no Azure"
  type        = string
  default     = "West Europe"

  validation {
    condition = contains([
      "West US 3",
      "East US",
      "Central US",
      "South Central US",
      "North Central US",
      "Brazil South",
      "West Europe",
      "North Europe"
    ], var.location)
    error_message = "A localização deve ser uma região válida do Azure."
  }
}

variable "environment" {
  description = "Ambiente (dev, staging, prod)"
  type        = string
  default     = "dev"

  validation {
    condition     = contains(["dev", "staging", "prod"], var.environment)
    error_message = "O ambiente deve ser: dev, staging ou prod."
  }
}

variable "tags" {
  description = "Tags adicionais para aplicar aos recursos"
  type        = map(string)
  default     = {}
}

variable "fiap_base_rg_name" {
  description = "Nome do Resource Group base para o FASTFOOD"
  type        = string
  default     = "rg-fiap-fastfood"
}

variable "storage_account_name" {
  description = "FastFood Storage Account"
  type        = string
  default     = "storagefiapfastfood"
}
variable "terraform_storage_container_name" {
  description = "FastFood Storage Account"
  type        = string
  default     = "fastfood-tfstate"
}