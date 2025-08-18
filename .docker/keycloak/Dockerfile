
FROM quay.io/keycloak/keycloak:24.0

# Copy realm export for automatic import
COPY realm-export.json /opt/keycloak/data/import/realm-export.json

# Expor porta padrão do Keycloak
EXPOSE 8080

# Use production start with realm import
CMD ["start", "--import-realm"]
