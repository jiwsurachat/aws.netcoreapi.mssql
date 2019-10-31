$> aws ecs register-task-definition --cli-input-json file://taskdef.json

$> aws ecs create-service --service-name netcoreapi --cli-input-json file://create-service.json