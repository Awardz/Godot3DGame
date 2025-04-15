extends Node3D

@export var is_reflective : bool 
@export var can_hurt_player : bool


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass


func _on_area_3d_body_entered(body: Node3D) -> void:
	if body.is_in_group("Player"):
		print("Player has entered body")
		if can_hurt_player:
			print("player takes damage")
		
		
	if body.is_in_group("Bullet"):
		print("Bullet has entered body")
		if is_reflective:
			print("bullet reflected")
