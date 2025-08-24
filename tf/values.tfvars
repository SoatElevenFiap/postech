# Terraform Variables for FastFood System
# Arquivo de valores para teste

# Resource Group
resource_group_name = "rg-fastfood-postech"
fiap_base_rg_name   = "rg-fiap-fastfood"

# Location
location = "West Europe"

# Environment
environment = "dev"

# Storage Account
storage_account_name               = "storagefiapfastfood"
terraform_storage_container_name   = "fastfood-tfstate"

# Tags
tags = {
  Environment = "dev"
  Project     = "FastFood-System"
  Owner       = "Equipe-FIAP"
  CreatedBy   = "Terraform"
  Course      = "Postech-FIAP"
  Purpose     = "Testing"
}

# ============================================
# VNet Configuration
# ============================================

# Virtual Network
vnet_name         = "vnet-fastfood-postech"
vnet_address_space = ["10.0.0.0/16"]

# Subnets
app_subnet_prefixes     = ["10.0.1.0/24"]
db_subnet_prefixes      = ["10.0.2.0/24"]
gateway_subnet_prefixes = ["10.0.255.0/27"]

# VNet Features
create_gateway_subnet       = false
enable_container_delegation = true
admin_source_address_prefix = "*"  # Para teste - em produção usar IP específico
